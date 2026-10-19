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
### Delete machines.
- [x] DELETE api/machines/id
- [x] Delete function
- [x] Add delete button
### Relationships: Transactions, Stores and Machines
- [ ] Define Entity Transaction
- [ ] Add foreign keys to Transaction
- [ ] Add Navigation properties to Transaction
- [ ] Configure relationships in OnModelCreating
### Improve machine list
- [ ] Add filtering.
- [ ] Add sorting.
### Import machines from SAP