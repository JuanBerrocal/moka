# Moka roadmap

## Store import polishing
- [ ] Import distribution channel
- [ ] Check duplicate SAP codes in csv file

## React routes component
- [x] install react-route-dom
- [x] Inside core folder, create api folder and api client
- [x] Configure path alias @
- [x] Inside core folder, create router folder and router component
- [x] Scenes folder. Create StoreList scene
- [x] Pods-Features folder. Create StoreList component.
- [x] Add a return button in the StoreListScene

## Stores CRUD
- [x] StoreListItem component
- [ ] Add search components to StoreList scene.
- [ ] Add filter components to StoreList scene.
- [ ] Add sorting components to StoreList component
- [x] Create StoreDetailScene
- [x] Create StoreForm (plain React).
- [x] POST a new Store
- [x] DELETE a Store
- [x] EDIT/PUT a Store
- [ ] View an Store.
- [ ] StoreImportScene
- [ ] StoreDetaiScene: Review and improve the returned creating/editing component 
- [ ] StoreDetaiScene: Check all the error messages and situations when saving/reading stores

## General
- [ ] Create common folder and navBar component

## Machines
### Machines backend and list.
- [x] Define entity
- [x] MachineDTO
- [x] GET api/machines
### Show machines detail
- [x] GET api/machines/id
- [x] Build MachineDetailScene
- [x] Build MachineForm.
- [x] Link route. Show Machines Detail
### Edit MAchines
- [x] PUT api/machines/id
- [x] MachineForm to update.  Save button.
- [x] Link route.
- [x] Field machine__type doesnt get the value read from the database in the form combo. Resolve it.
- [x] Error message in the MachineForm gives the description of the error (f.ex.value cant be null but doesnt inform about what column needs review).
### Create MAchines
- [x] POST api/machines
- [x] Adapt MachineForm to create. Save new form.
- [x] Link route.
- [x] Write a CreateMachineDto. Use it in the MachineController.
### Delete machines.
- [x] DELETE api/machines/id
- [x] Delete function
- [x] Add delete button
### Relationships: Transactions, Stores and Machines
- [x] Define Entity Transaction
- [x] Add foreign keys to Transaction
- [x] Add Navigation properties to Transaction
- [x] Configure relationships in OnModelCreating
- [x] Migration to database
### Transactions and transaction lines
- [x] Define transaction Entity and TransactionLine entity
- [x] Define relationships between Transaction and TransactionLine
- [ ] Endpoint to create one transaction and its transaction lines in one request.
### CreateTransaction validation checklist

#### 1. Transaction header validations

- [ ] Request body must not be null.
- [x] `OriginId` must be a positive integer.
- [x] `DestinationId` must be a positive integer.
- [ ] Origin store must exist in the database.
- [ ] Destination store must exist in the database.
- [ ] Origin and destination must be different stores (if required by the movement type).
- [ ] `Date` must be a valid business date.
- [ ] Decide whether future transaction dates are allowed.
- [ ] `MovementType` must be a defined enum value.
- [ ] `MovementType` must not be `Unknown`.
- [ ] `Process` must not exceed 40 characters.
- [ ] `DocumentNumber` must not exceed 20 characters.
- [ ] `Notes` must respect any length limit defined by the business.
- [ ] `Temporary` must be a valid boolean value.

#### 2. Transaction lines validations

- [ ] `TransactionLines` must not be null.
- [ ] Transaction must contain at least one line.
- [x] Every `MachineId` must be a positive integer.
- [ ] Every referenced machine must exist in the database.
- [ ] A machine cannot appear more than once in the same transaction.
- [ ] Each line's `Notes` must respect any length limit defined by the business.
- [ ] `LineId` must be assigned by the backend.
- [ ] `LineId` values must be unique within the transaction.

#### 3. Machine location validations

- [ ] Check the machine's currently recorded `StoreId`.
- [ ] Compare it with the transaction's declared `OriginId`.
- [ ] If they differ, show a warning to the user.
- [ ] Do not block the transaction because of this discrepancy.
- [ ] Every employee is currently allowed to proceed.
- [ ] Consider superuser authorization for this discrepancy in a future version.

#### 4. Transaction creation

- [ ] Map the DTO header properties to a new `Transaction`.
- [ ] Map each DTO line to a new `TransactionLine`.
- [ ] Assign sequential `LineId` values, starting at 1.
- [ ] Associate each line with its `MachineId`.
- [ ] Associate all lines with the new transaction.
- [ ] Normalize `Process` and `DocumentNumber` using `ToUpperInvariant()` if required.

#### 5. Update machine locations

- [ ] Update every involved machine's `StoreId`.
- [ ] Set the new `StoreId` to the transaction's `DestinationId`.
- [ ] Ensure all involved machines are updated, not just the first one.

#### 6. Database persistence

- [ ] Add the transaction to the EF Core context.
- [ ] Save the transaction, its lines and machine updates together.
- [ ] Use one `SaveChangesAsync()` operation.
- [ ] Ensure database failures are logged.
- [ ] Return HTTP 201 when creation succeeds.
- [ ] Return appropriate HTTP 400 responses for invalid input.
- [ ] Return an appropriate server error for unexpected failures.

#### 7. Future improvements

- [ ] Add superuser authorization for machine-location discrepancies.
- [ ] Define whether document numbers must be unique.
- [ ] Define whether machines can be included in multiple active temporary transactions.
- [ ] Define movement-type-specific validation rules.
- [ ] Add automated tests for successful and unsuccessful transactions.

---

#### Important business rules

1. A transaction must contain at least one machine.
2. Origin and destination stores must exist.
3. Machines must exist.
4. Duplicate machines are not allowed within the same transaction.
5. A machine's recorded location does not have to match the declared origin.
6. All employees can currently proceed despite location discrepancies.
7. After a successful transaction, all involved machines must have their current location updated to the destination.
8. Transaction history must preserve the declared origin and destination.

### Improve machine list
- [ ] Add filtering.
- [ ] Add sorting.
### Import machines from SAP