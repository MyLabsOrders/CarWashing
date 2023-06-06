import { Box, Container, Stack, Typography } from "@mui/material";
import "./Navlist.css";
import { useState, useEffect } from "react";
import { getCookie } from "typescript-cookie";
import { authorizeAdmin } from "../../lib/identity/identity";
import { green } from "@mui/material/colors";
import NavigateBtn from "./NavigateBtn";


const Navlist = () => {
    const [isAdmin, setIsAdmin] = useState(false);

    useEffect(() => {
        getUser();
    }, []);

    const getUser = async () => {
        try {
            await authorizeAdmin(
                getCookie("jwt-authorization") ?? "",
                getCookie("current-username") ?? ""
            );
            setIsAdmin(true);
        } catch (error) {
            setIsAdmin(false);
        }
    };

    return (
        <Box
            maxWidth="inherited"
            color="#fff"
            top={0}
            py={2}
            boxShadow={0}
            position="fixed"
            width="30%"
            height={"100%"}
            zIndex={1}
            sx={{ backdropFilter: "blur(2px)", background: green[700] }}>
            <Container
                sx={{
                    display: "flex",
                    flexDirection: "column",
                    justifyContent: "space-between",
                    alignItems: "center",
                }}>
                <Typography
                    variant="h5"
                    textTransform={"uppercase"}
                    className="logo"
                    marginTop={"2rem"}>
                    Car washing
                </Typography>
                <Stack direction="column" spacing={2} marginTop={"5rem"}>
                    <NavigateBtn to="/" body="Главная" />
                    <NavigateBtn to="/profile" body="Профиль" />
                    <NavigateBtn to="/documents" body="Документы" />
                    <NavigateBtn to="/login" body="Вход" />
                    <NavigateBtn to="/signup" body="Регистрация" />
                    {isAdmin && <NavigateBtn to="/admin" body="Админ" />}
                </Stack>
            </Container>
        </Box>
    );
};

export default Navlist;

