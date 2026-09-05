
import {BrowserRouter, Routes, Route} from "react-router-dom";
import {HomeScene} from "@/scenes/home";
import {StoreListScene, StoreDetailScene} from "@/scenes/stores";

export const RouterComponent: React.FC = () => {
    return <BrowserRouter>
        <Routes>
            <Route path="/" element={<HomeScene />} />
            <Route path="/stores" element={<StoreListScene />} />
            <Route path="/stores/new" element={<StoreDetailScene />} />
        </Routes>
    </BrowserRouter>
}