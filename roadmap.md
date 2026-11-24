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

## Transactions
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

- [x] Request body must not be null.
- [x] `OriginId` must be a positive integer.
- [x] `DestinationId` must be a positive integer.
- [x] Origin store must exist in the database.
- [x] Destination store must exist in the database.
- [x] Origin and destination must be different stores (if required by the movement type).
- [x] `Date` must be a valid business date.
- [x] Decide whether future transaction dates are allowed.
- [x] `MovementType` must be a defined enum value.
- [x] `MovementType` must not be `Unknown`.
- [x] `Process` must not exceed 40 characters.
- [x] `DocumentNumber` must not exceed 20 characters.
- [ ] `Notes` must respect any length limit defined by the business.
- [x] `Temporary` must be a valid boolean value.

#### 2. Transaction lines validations

- [x] `TransactionLines` must not be null.
- [x] Transaction must contain at least one line.
- [x] Every `MachineId` must be a positive integer.
- [x] Every referenced machine must exist in the database.
- [x] A machine cannot appear more than once in the same transaction.
- [ ] Each line's `Notes` must respect any length limit defined by the business.
- [x] `LineId` must be assigned by the backend.
- [x] `LineId` values must be unique within the transaction.

#### 3. Machine location validations

- [x] Check the machine's currently recorded `StoreId`.
- [x] Compare it with the transaction's declared `OriginId`.
- [x] If they differ, create a warning for the user.
- [ ] Show the warning to the user.
- [x] Do not block the transaction because of this discrepancy.
- [ ] Every employee is currently allowed to proceed.
- [ ] Consider superuser authorization for this discrepancy in a future version.

#### 4. Transaction creation

- [x] Map the DTO header properties to a new `Transaction`.
- [x] Map each DTO line to a new `TransactionLine`.
- [x] Assign sequential `LineId` values, starting at 1.
- [x] Associate each line with its `MachineId`.
- [x] Associate all lines with the new transaction.
- [x] Normalize `Process` and `DocumentNumber` using `ToUpperInvariant()` if required.

#### 5. Update machine locations

- [x] Update every involved machine's `StoreId`.
- [x] Set the new `StoreId` to the transaction's `DestinationId`.
- [x] Ensure all involved machines are updated, not just the first one.

#### 6. Database persistence

- [x] Add the transaction to the EF Core context.
- [x] Save the transaction, its lines and machine updates together.
- [x] Use one `SaveChangesAsync()` operation.
- [x] Ensure database failures are logged.
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


1. Backend — ASP.NET Core
Architecture and configuration
- [ ] MUST — Review and clean the project structure.
- [ ] MUST — Use dependency injection correctly.
- [ ] MUST — Configure Entity Framework Core and MokaDbContext.
- [ ] MUST — Configure SQL Server connection through configuration files/environment variables.
- [ ] MUST — Ensure development and production configurations are separated.
- [ ] MUST — Configure proper error handling and HTTP status codes.
- [ ] MUST — Configure logging.
- [ ] — Add Swagger/OpenAPI documentation.
- [ ] — Remove unused code, dependencies and debugging artefacts.
Database and Entity Framework
- [ ] MUST — Review all entities and their properties.
- [ ] MUST — Verify primary keys and foreign keys.
- [ ] MUST — Verify entity relationships and navigation properties.
- [ ] MUST — Configure entity constraints and validation.
- [ ] MUST — Create and verify database migrations.
- [ ] MUST — Ensure the database can be created from a clean environment.
- [ ] — Add seed data for demonstration purposes.
API and business logic
- [ ] MUST — Complete CRUD operations for the main entities.
- [ ] MUST — Verify all API endpoints and routes.
- [ ] MUST — Implement input validation.
- [ ] MUST — Handle missing resources and invalid requests.
- [ ] MUST — Implement correct relationship handling.
- [ ] MUST — Prevent accidental deletion of related records.
- [ ] — Implement pagination where appropriate.
- [ ] — Implement filtering and sorting.
- [ ] — Implement business rules and prevent invalid operations.
- [ ] — Review naming conventions and consistency.
Authentication and security
- [ ] MUST — Implement JWT authentication.
- [ ] MUST — Verify login functionality.
- [ ] MUST — Protect private API endpoints.
- [ ] MUST — Validate tokens correctly.
- [ ] MUST — Hash passwords securely.
- [ ] MUST — Keep secrets out of Git and source code.
- [ ] — Implement role-based authorisation if roles are part of Moka's scope.
- [ ] — Review CORS configuration.
2. Frontend — React
Structure and architecture
- [ ] MUST — Review component organisation.
- [ ] MUST — Separate API communication from presentation components.
- [ ] MUST — Centralise API configuration.
- [ ] MUST — Implement consistent state management.
- [ ] MUST — Remove unused components and dependencies.
Functionality
- [ ] MUST — Implement login and logout.
- [ ] MUST — Connect all essential screens to the backend.
- [ ] MUST — Complete the main CRUD workflows.
- [ ] MUST — Display related entities correctly.
- [ ] MUST — Implement forms with validation.
- [ ] MUST — Display useful error messages.
- [ ] MUST — Handle loading and empty states.
- [ ] MUST — Handle expired or invalid authentication tokens.
- [ ] — Add search, filtering and pagination if supported by the backend.
User experience
- [ ] MUST — Provide consistent navigation.
- [ ] MUST — Ensure the main screens are visually consistent.
- [ ] MUST — Make the application usable on desktop.
- [ ] — Improve mobile responsiveness.
- [ ] — Add confirmation dialogs for destructive operations.
- [ ] — Improve accessibility and keyboard navigation.
3. Testing and quality
- [ ] MUST — Test the main backend services and business logic.
- [ ] MUST — Test important API endpoints.
- [ ] MUST — Test authentication and authorisation.
- [ ] MUST — Test database relationships.
- [ ] MUST — Test the main frontend workflows manually.
- [ ] MUST — Test invalid input and error scenarios.
- [ ] MUST — Test the application from a clean installation.
- [ ] — Add automated frontend tests.
- [ ] — Add integration tests for critical workflows.
4. Deployment and demonstration
- [ ] MUST — Prepare a clean repository on GitHub.
- [ ] MUST — Write a professional README.
- [ ] MUST — Explain the architecture and technology stack.
- [ ] MUST — Include database setup instructions.
- [ ] MUST — Include instructions for running the backend and frontend.
- [ ] MUST — Provide demonstration data or a reproducible setup.
- [ ] MUST — Ensure the application starts and works on another machine or clean environment.
- [ ] MUST — Include screenshots of the main screens.
- [ ] MUST — Prepare a short demonstration video.
- [ ] — Deploy a public demonstration if feasible.
- [ ] — Add an architecture diagram.
5. Professional presentation
- [ ] MUST — Prepare a concise explanation of Moka's purpose.
- [ ] MUST — Be able to explain the backend architecture.
- [ ] MUST — Explain how EF Core and database relationships work.
- [ ] MUST — Explain JWT authentication.
- [ ] MUST — Explain how React communicates with the API.
- [ ] MUST — Be able to discuss technical decisions and trade-offs.
- [ ] MUST — Be able to explain what you would improve next.
- [ ] MUST — Add Moka to your CV and LinkedIn portfolio.
- [ ] — Prepare answers to likely technical interview questions.