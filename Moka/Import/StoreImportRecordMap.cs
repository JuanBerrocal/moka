using CsvHelper.Configuration;

namespace Moka.Import
{
    public class StoreImportRecordMap : ClassMap<StoreImportRecord>
    {

        public StoreImportRecordMap() {

            Map(m => m.SapCode).Name("Deudor");
            Map(m => m.Name).Name("Nombre 1");
            Map(m => m.TradeName).Name("Nombre 2");
            Map(m => m.Address).Name("Calle");
            Map(m => m.PostalCode).Name("CP");
            Map(m => m.City).Name("Población");
            Map(m => m.TaxId).Name("N.I.F. comunitario");
        }
    }
}
