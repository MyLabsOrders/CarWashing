import {
    Box,
    Button,
    CardMedia,
    Dialog,
    Divider,
    Stack,
    Typography,
} from "@mui/material";
import { green, grey } from "@mui/material/colors";
import { PurchaseForm } from "../PurchaseForm";
import { useState } from "react";
import { addProduct } from "../../../lib/users/users";
import { getCookie } from "typescript-cookie";
import { Notification } from "../../../features";
import { useNavigate } from "react-router-dom";

export interface IProduct {
    id: string;
    name: string;
    total: number;
    status: string;
    image: string;
}

export const createProduct = (
    id: string,
    name: string,
    total: number,
    status: string,
    image: string
): IProduct => {
    return { id, name, total, status, image };
};

export const createProducts = (count: number): IProduct[] => {
    const result: IProduct[] = new Array(count)
        .fill(null)
        .map((_, i) =>
            createProduct(
                `${i}`,
                `Имя${i}`,
                1000 + 100 * i,
                `Статус${i}`,
                "https://loremflickr.com/640/360"
            )
        );
    console.log(result);

    return result;
};

const Product = ({ id, name, total, status, image }: IProduct) => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [message, setMessage] = useState<string | null>(null);
    const navigate = useNavigate();

    const open_modal_click = () => {
        setIsModalOpen(true);
    };

    const close_modal_click = async () => {
        setIsModalOpen(false);
    };

    const submit_processing = async () => {
        try {
            setMessage(null);
            await addProduct(
                getCookie("jwt-authorization") ?? "",
                getCookie("current-user") ?? "",
                id
            );
            setMessage("Successfully bought it!");
        } catch (error: any) {
            setMessage(error?.response?.data?.Detailes);
            navigate("/profile");
        }
    };

    return (
        <>
            {message && message.includes("bought") && (
                <Notification message={message} type={"success"} />
            )}
            {message && !message.includes("bought") && (
                <Notification message={message} type={"warning"} />
            )}
            <Box
                width={"50%"}
                sx={{
                    p: 2,
                    display: "flex",
                    alignItems: "center",
                    backgroundColor: "transparent",
                }}>
                <CardMedia component="img" src={image} alt={name} sx={{ boxShadow: "0 0 5px 5px #1b5e20", borderRadius: "5px", height: "100%", width: "auto" }} />
            </Box>
            <Divider sx={{ backgroundColor: "white" }} />
            <Box
                width={"100%"}
                sx={{
                    p: 2,
                    display: "flex",
                    justifyContent: "space-between",
                    flexDirection: "column",
                }}>
                <Stack
                    maxWidth={"inherit"}
                    padding={0}
                    spacing={0.5}
                    sx={{ flex: 1, marginLeft: "10px" }}>
                    <Typography variant="h5" color={grey[900]}>
                        {name}
                    </Typography>
                    <Typography variant="body1" color={grey[900]}>
                        Total: {total}
                    </Typography>
                </Stack>
                <div
                    style={{
                        display: "flex",
                        justifyContent: "flex-end",
                        alignItems: "flex-end",
                    }}>
                    <Button
                        sx={{
                            "&:hover": {
                                backgroundColor: green[500],
                                color: "white",
                            },
                        }}
                        onClick={open_modal_click}>
                        Purchase
                    </Button>
                </div>
            </Box>
            <Dialog
                open={isModalOpen}
                onClose={close_modal_click}
                sx={{
                    backdropFilter: "blur(5px)",
                    "& .MuiPaper-root": {
                        borderRadius: 3,
                        bgcolor: "#132f4b",
                    },
                }}>
                <PurchaseForm total={total} onSubmit={submit_processing} />
            </Dialog>
        </>
    );
};
export default Product;

