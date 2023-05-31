import { Route, Routes } from "react-router-dom";
import { HomePage as Home, LoginPage as Login, ProfilePage as Profile, RegisterPage as SignUp } from "./pages";
import { Container } from "@mui/material";
import { Menu } from "./features";
import { DocsPage as Docs } from "./pages/DocsPage";
import { AdminPage as Admin } from "./pages/AdminPage";
import { useEffect, useState } from "react";
import { getIdentityUser as get_user_identity } from "./lib/identity/identity";
import { getCookie } from "typescript-cookie";

function App() {
    const [isAdmin, setIsAdmin] = useState(true);   

    useEffect(() => {
        fetch_user_data();
    }, []);

    const fetch_user_data = async () => {
        try {
            const { data } = await get_user_identity(
                getCookie("jwt-authorization") ?? "",
                getCookie("current-user") ?? ""
            );
            setIsAdmin(data.role === "admin");
        } catch (error) {}
    };

    return (
        <>
            <Container>
                <Menu />
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/signup" element={<SignUp />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/profile" element={<Profile />} />
                    <Route path="/documents" element={<Docs />} />
                    <Route
                        path="/admin"
                        element={isAdmin ? <Admin /> : <Home />}
                    />
                </Routes>
            </Container>
        </>
    );
}

export default App;

