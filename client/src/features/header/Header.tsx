import { Box, Button, CardMedia, Drawer } from "@mui/material";
import { Navlist } from "../../layouts";
import "./Header.css";
import { useState } from "react";
import MenuIcon from "@mui/icons-material/Menu";
import { green } from "@mui/material/colors";
import CarWashLogo from "../../assets/CarWashLogo.png";

const Menu = () => {
    const [isOpen, setIsOpen] = useState<boolean>();

    return (
        <Box width={"100%"} sx={{ background: green[400], display: "flex", flexDirection: "row" }} position={"fixed"}>
            <Button
                onClick={() => setIsOpen(true)}
                sx={{
                    "&:hover": {
                        background: green[800],
                    },
                }}>
                <MenuIcon></MenuIcon>
            </Button>
            <CardMedia
                component="img"
                src={CarWashLogo}
                alt=""
                sx={{
                    width: "4rem",
                    marginRight: "1rem",
                    opacity: 0.6,
                    userSelect: "none",
                }}
            />
            <Drawer
                anchor={"left"}
                open={isOpen}
                onClose={() => setIsOpen(false)}>
                <Navlist />
            </Drawer>
        </Box>
    );
};

export default Menu;
