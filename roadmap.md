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
- [ ] GET api/machines/id
- [ ] Build MachineDetailScene
- [ ] Build MachineForm.
- [ ] Link route. Show Machines Detail
### Edit MAchines
- [ ] PUT api/machines/id
- [ ] MachineForm to update.  Save button.
- [ ] Link route.
### Create MAchines
- [x] POST api/machines
- [ ] Adapt MachineForm to create. Save new form.
- [ ] Link route.
### Delete machines.
- [ ] DELETE api/machines/id
- [ ] Delete function
- [ ] Add delete button
### Improve machine list
- [ ] Add filtering.
- [ ] Add sorting.
### Import machines from SAP