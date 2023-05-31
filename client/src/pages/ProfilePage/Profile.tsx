import { Box, Button, Dialog, Stack } from "@mui/material";
import { ItemTable, ProfileForm } from "../../components";
import { useEffect, useState } from "react";
import { getCookie } from "typescript-cookie";
import { IProductPage } from "../../shared";
import { Notification } from "../../features";
import { useLocation } from "react-router-dom";
import { getCheque, getHistory } from "../../lib/products/products";
import { createProduct } from "../../shared/models/product/IProduct";
import { getUser } from "../../lib/users/users";
import { green } from "@mui/material/colors";

const createProducts = (count: number): IProductPage => {
    const orders = new Array(count)
        .fill(null)
        .map((_, i) =>
            createProduct(
                `${i}`,
                `status${i}`,
                `name${i}`,
                "https://loremflickr.com/640/360",
                0,
                `01.01.000${i}`
            )
        );

    return { orders, page: 0, totalPage: 2 };
    // return orders;
};

const Profile = () => {
    const [error, setError] = useState<string | null>(null);
    const [basketProducts, setBasketProducts] = useState<IProductPage | null>(
        createProducts(5)
    );
    const [historyProducts, setHistoryProducts] = useState<IProductPage | null>(
        createProducts(5)
    );
    const [isOrderBasketOpen, setOrderBasketOpen] = useState<boolean>(false);
    const [isHistoryOpen, setHistoryOpen] = useState<boolean>(false);
    const [documentLink, setDocumentLink] = useState<string>();

    const location = useLocation();
    const message = location.state && location.state.message;
    const type = location.state && location.state.type;

    useEffect(() => {
        fetch_items();
        fetch_history();
    },[]);

    const fetch_items = () => {
        (async () => {
            try {
                const { data } = await getUser(
                    getCookie("current-user") ?? "",
                    getCookie("jwt-authorization") ?? ""
                );
                setBasketProducts({ orders: data.orders, page: 1, totalPage: 1 });
                window.open(documentLink, "_blank", "noreferrer");
            } catch (error) {
                setBasketProducts(null);
            }
        })();
    };

    const fetch_history = async () => {
        try {
            const history_orders = await getHistory();
            setHistoryProducts(history_orders);
        } catch (error) {
        }
    }

    const fetch_cheque = async () => {
        try {
            const { data } = await getCheque(
                getCookie("jwt-token") ?? "",
                getCookie("order-date") ?? ""
            );
            setDocumentLink(data.link);
        } catch (error) {
        }
    };

    return (
        <Box
            bgcolor={green[900]}
            display={"flex"}
            alignItems={"center"}
            justifyContent={"center"}
            height={"100vh"}
            width={"100vw"}>
            {error && <Notification message={error} type="error" />}
            {message && <Notification message={message} type={type} />}
            <Box
                display={"flex"}
                flexDirection={"column"}
                justifyContent={"space-evenly"}
                alignItems={"center"}
                width={"50%"}
            >
                <ProfileForm setError={setError} />
                <Stack spacing={"2rem"} marginTop={"2rem"} width={"14rem"}>
                    <Button
                        onClick={fetch_cheque}
                        sx={{
                            background: green[500],
                            borderRadius: "5px",
                            ":hover": { background: green[300] },
                        }}>
                        Получить чек
                    </Button>
                    <Button
                        onClick={() => setOrderBasketOpen(true)}
                        sx={{
                            background: green[500],
                            borderRadius: "5px",
                            ":hover": { background: green[300] },
                        }}>
                        Просмотреть корзину
                    </Button>
                    <Button
                        onClick={() => setHistoryOpen(true)}
                        sx={{
                            background: green[500],
                            borderRadius: "5px",
                            ":hover": { background: green[300] },
                        }}>
                        Просмотреть историю
                    </Button>
                </Stack>
                <Dialog open={isOrderBasketOpen} onClose={() => setOrderBasketOpen(false)}>
                    <ItemTable
                        products={
                            basketProducts ?? { orders: [], page: 0, totalPage: 0 }
                        }
                    />
                </Dialog>
                <Dialog open={isHistoryOpen} onClose={() => setHistoryOpen(false)}>
                    <ItemTable
                        products={
                            historyProducts ?? { orders: [], page: 0, totalPage: 0 }
                        }
                    />
                </Dialog>
                {/* <Dialog open={isModalOpen}>
                    <Box width={"20rem"} padding={"1rem"} bgcolor={"#001e3c"}>
                        <Typography fontSize={32} marginBottom={"1rem"} color={"white"}>
                            Спасибо за заказ!
                        </Typography>
                        <Stack justifyContent={"space-between"} direction={"row"}>
                            <Button
                                onClick={closeModal}
                                sx={{
                                    backgroundColor: red[500],
                                }}>
                                Закрыть
                            </Button>
                            <Link
                                underline="none"
                                href={documentLink}>
                                <Button
                                sx={{
                                    backgroundColor: green[500],
                                }}>
                                Просмотреть чек
                            </Button>
                            </Link>
                        </Stack>
                    </Box>
                </Dialog> */}
            </Box>
        </Box>
    );
};

export default Profile;

