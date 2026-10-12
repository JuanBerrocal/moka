
import {BrowserRouter, Routes, Route} from "react-router-dom";
import {HomeScene} from "@/scenes/home";
import {StoreListScene, StoreDetailScene} from "@/scenes/stores";
import {MachineListScene, MachineDetailScene} from "@/scenes/machines";

export const RouterComponent: React.FC = () => {
    return <BrowserRouter>
        <Routes>
            <Route path="/" element={<HomeScene />} />
            <Route path="/stores" element={<StoreListScene />} />
            <Route path="/stores/new" element={<StoreDetailScene />} />
            <Route path="/stores/:id/edit" element={<StoreDetailScene />} />
            <Route path="/machines" element={<MachineListScene />} />
            <Route path="/machines/new" element={<MachineDetailScene />} />
            <Route path="/machines/:id/edit" element={<MachineDetailScene />} />
        </Routes>
    </BrowserRouter>
}