import { Box, Button, Drawer } from "@mui/material";
import { Navlist } from "../../layouts";
import "./Header.css";
import { useState } from "react";
import MenuIcon from "@mui/icons-material/Menu";
import { green } from "@mui/material/colors";

const Menu = () => {
    const [isOpen, setIsOpen] = useState<boolean>();

    return (
        <Box width={"100%"} sx={{ background: green[400] }} position={"fixed"}>
            <Button
                onClick={() => setIsOpen(true)}
                sx={{
                    "&:hover": {
                        background: green[800],
                    },
                }}>
                <MenuIcon></MenuIcon>
            </Button>
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

