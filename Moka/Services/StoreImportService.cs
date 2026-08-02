
using CsvHelper.Configuration;
using CsvHelper;
using Moka.Data;
using Moka.Import;
using Moka.Models.Enums;
using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Moka.Models;
using System.Net;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace Moka.Services
{
    public class StoreImportService : IStoreImportService
    {
        private readonly MokaDbContext _mokaDbContext;

        public StoreImportService(MokaDbContext context)
        {
            _mokaDbContext = context;
        }
        public async Task<StoreImportResult> ImportAsync(IFormFile file)
        {

            ArgumentNullException.ThrowIfNull(file);

            using var reader = new StreamReader(file.OpenReadStream());
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
            using var csv = new CsvReader(reader, configuration);

            var result = new StoreImportResult();
            result.FileName = file.FileName;

            csv.Context.RegisterClassMap<StoreImportRecordMap>();
            
            // Get records to import.
            try
            {
                var records = csv.GetRecords<StoreImportRecord>().ToList();
                var sapStores = await _mokaDbContext.Stores.Where(s => s.Source == StoreSource.Sap).ToDictionaryAsync(s => s.SapCode!);

                for (int i = 0; i < records.Count; i++)
                {

                    var record = records[i];

                    if (string.IsNullOrWhiteSpace(record.Name))
                    {
                        result.Skipped++;
                        result.Errors.Add($"Line: {i + 2}. Missing store name");
                    }
                    else if (string.IsNullOrWhiteSpace(record.SapCode))
                    {
                        result.Skipped++;
                        result.Errors.Add($"Line: {i + 2}. Missing SAP code");
                    }
                    else if (sapStores.TryGetValue(record.SapCode, out var store))
                    {
                        // The client from SAP exists in our database. Only need to update it.

                        store.Name = record.Name;
                        store.TradeName = record.TradeName;
                        store.Address = record.Address;
                        store.PostalCode = record.PostalCode;
                        store.City = record.City;
                        store.TaxId = record.TaxId;

                        result.Updated++;
                    }
                    else
                    {

                        var newStore = new Store
                        {
                            Name = record.Name,
                            SapCode = record.SapCode,
                            TradeName = record.TradeName,
                            Address = record.Address,
                            PostalCode = record.PostalCode,
                            City = record.City,
                            TaxId = record.TaxId,
                            Source = StoreSource.Sap
                        };

                        _mokaDbContext.Stores.Add(newStore);
                        result.Created++;

                        // Add the new Store to our store list.
                        sapStores.Add(newStore.SapCode, newStore);
                    }

                }
            }
            catch (HeaderValidationException ex)
            {
                foreach (var invalidHeader in ex.InvalidHeaders) 
                {
                    result.Errors.Add($"Missing header column: {invalidHeader.Names[0]}");
                }
                return result;
            }
            
            await _mokaDbContext.SaveChangesAsync();
            return result;
        }
    }
}
