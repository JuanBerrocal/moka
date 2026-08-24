import react from "react";
import {BrowserRouter, Routes, Route} from "react-router-dom";
import {HomeScene} from "@/scenes/home";

export const RouterComponent: React.FC = () => {
    return <BrowserRouter>
        <Routes>
            <Route path="/" element={<HomeScene />} />
        </Routes>
    </BrowserRouter>
}