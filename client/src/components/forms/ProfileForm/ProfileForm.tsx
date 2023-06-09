import {
    Avatar,
    Box,
    Button,
    Card,
    Dialog,
    Divider,
    IconButton,
    Stack,
    TextField,
    Typography,
} from "@mui/material";
import { green, grey } from "@mui/material/colors";
import { ReplenishForm } from "../ReplenishForm";
import EditIcon from "@mui/icons-material/Edit";
import SaveIcon from "@mui/icons-material/Save";
import { useCallback, useEffect, useState } from "react";
import { changeUserProfile, getUser } from "../../../lib/users/users";
import { getCookie } from "typescript-cookie";
import { useNavigate } from "react-router-dom";
import { IUser } from "../../../shared";

export interface IProfileProps {
    setError: (error: string | null) => void;
}

const ProfileForm = ({ setError }: IProfileProps) => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [isEditing, setIsEditing] = useState(false);
    const [editedFirstName, setEditedFirstName] = useState("");
    const [editedMiddleName, setEditedMiddleName] = useState("");
    const [editedLastName, setEditedLastName] = useState("");
    const [editedBirthDate, setEditedBirthDate] = useState("");
    const [editedPhoneNumber, setEditedPhoneNumber] = useState("");
    const [user, setUser] = useState<IUser>({
        id: "",
        firstName: "first name",
        middleName: "middle name",
        lastName: "last name",
        birthDate: "00-00-0000",
        number: "phone number",
        image: "",
        gender: "male",
        money: 0,
        orders: [],
    });
    const navigate = useNavigate();

    useEffect(() => {
        setEditedFirstName(user.firstName);
        setEditedMiddleName(user.middleName);
        setEditedLastName(user.lastName);
        setEditedBirthDate(user.birthDate);
        setEditedPhoneNumber(user.number);
    }, [user]);

    const fetchData = useCallback(async () => {
        try {
            setError(null);
            const { data } = await fetchUser();
            setUser(data);
        } catch (error: any) {
            setError("You are not authorized");
        }
    },[setError]);

    const fetchUser = async () => {
        return await getUser(
            getCookie("current-user") ?? "",
            getCookie("jwt-authorization") ?? ""
        );
    };

    useEffect(() => {
        (async () => await fetchData())();
    },[fetchData]);

    const handleOpen = () => {
        setIsModalOpen(true);
    };

    const handleClose = () => {
        setIsModalOpen(false);
    };

    const handleEditClick = () => {
        setIsEditing(true);
    };

    const handleSaveClick = async () => {
        setIsEditing(false);

        try {
            await changeUserProfile(
                getCookie("jwt-authorization") ?? "",
                getCookie("current-user") ?? "",
                {
                    identityId: user.id,
                    firstName: editedFirstName,
                    middleName: editedMiddleName,
                    lastName: editedLastName,
                    phoneNumber: editedPhoneNumber,
                    userImage: user.image,
                    birthDate: editedBirthDate,
                    gender: user.gender,
                    isActive: true
                }
            );

            navigate("/profile", {
                state: {
                    message: "Successfully edited profile!",
                    type: "success",
                },
            });
        } catch (error: any) {
            navigate("/profile", {
                state: {
                    message: `${error?.response?.data?.Detailes}`,
                    type: "error",
                },
            });
        }
    };

    return (
        <Box className="profile" width={"100%"}>
            <Card
                sx={{
                    width: "100%",
                    backgroundColor: grey[900],
                    borderRadius: "10px",
                    position: "relative",
                    paddingTop: "3rem"
                }}>
                <IconButton
                    onClick={isEditing ? handleSaveClick : handleEditClick}
                    sx={{ display: "flex", position:"absolute", right: "30px" }}>
                    {isEditing ? (
                        <SaveIcon sx={{ color: "white" }} />
                    ) : (
                        <EditIcon sx={{ color: "white" }} />
                    )}
                </IconButton>
                <Box
                    sx={{
                        p: 2,
                        display: "flex",
                        alignItems: "center",
                        backgroundColor: grey[800],
                    }}>
                    <IconButton>
                        <Avatar variant="rounded" src={user.image} />
                    </IconButton>
                    <Stack spacing={0.5} sx={{ flex: 1, marginLeft: "10px" }}>
                        {isEditing ? (
                            <>
                                <TextField
                                    variant="standard"
                                    value={editedFirstName}
                                    onChange={(e) =>
                                        setEditedFirstName(e.target.value)
                                    }
                                    fullWidth
                                    InputProps={{
                                        style: {
                                            color: "white",
                                        },
                                    }}
                                    sx={{
                                        textAlign: "center",
                                        "& .MuiInputBase-root": {
                                            color: "white",
                                            "&:before": {
                                                borderBottomColor: "white",
                                            },
                                            "&:hover:before": {
                                                borderBottomColor: "white",
                                            },
                                            "&.Mui-focused:before": {
                                                borderBottomColor: "green",
                                            },
                                            "&.Mui-focused:after": {
                                                borderBottomColor: "green",
                                            },
                                        },
                                    }}
                                />
                                <TextField
                                    variant="standard"
                                    value={editedMiddleName}
                                    onChange={(e) =>
                                        setEditedMiddleName(e.target.value)
                                    }
                                    fullWidth
                                    InputProps={{
                                        style: {
                                            color: "white",
                                        },
                                    }}
                                    sx={{
                                        textAlign: "center",
                                        "& .MuiInputBase-root": {
                                            color: "white",
                                            "&:before": {
                                                borderBottomColor: "white",
                                            },
                                            "&:hover:before": {
                                                borderBottomColor: "white",
                                            },
                                            "&.Mui-focused:before": {
                                                borderBottomColor: "green",
                                            },
                                            "&.Mui-focused:after": {
                                                borderBottomColor: "green",
                                            },
                                        },
                                    }}
                                />
                                <TextField
                                    variant="standard"
                                    value={editedLastName}
                                    onChange={(e) =>
                                        setEditedLastName(e.target.value)
                                    }
                                    fullWidth
                                    InputProps={{
                                        style: {
                                            color: "white",
                                        },
                                    }}
                                    sx={{
                                        textAlign: "center",
                                        "& .MuiInputBase-root": {
                                            color: "white",
                                            "&:before": {
                                                borderBottomColor: "white",
                                            },
                                            "&:hover:before": {
                                                borderBottomColor: "white",
                                            },
                                            "&.Mui-focused:before": {
                                                borderBottomColor: "green",
                                            },
                                            "&.Mui-focused:after": {
                                                borderBottomColor: "green",
                                            },
                                        },
                                    }}
                                />
                                <TextField
                                    type="date"
                                    variant="standard"
                                    value={editedBirthDate}
                                    onChange={(e) => {
                                        setEditedBirthDate(e.target.value);
                                    }}
                                    fullWidth
                                    InputProps={{
                                        style: {
                                            color: "white",
                                        },
                                    }}
                                    sx={{
                                        textAlign: "center",
                                        "& .MuiInputBase-root": {
                                            color: "white",
                                            "&:before": {
                                                borderBottomColor: "white",
                                            },
                                            "&:hover:before": {
                                                borderBottomColor: "white",
                                            },
                                            "&.Mui-focused:before": {
                                                borderBottomColor: "green",
                                            },
                                            "&.Mui-focused:after": {
                                                borderBottomColor: "green",
                                            },
                                        },
                                    }}
                                />
                                <TextField
                                    variant="standard"
                                    value={editedPhoneNumber}
                                    onChange={(e) =>
                                        setEditedPhoneNumber(e.target.value)
                                    }
                                    fullWidth
                                    InputProps={{
                                        style: {
                                            color: "white",
                                        },
                                    }}
                                    sx={{
                                        textAlign: "center",
                                        "& .MuiInputBase-root": {
                                            color: "white",
                                            "&:before": {
                                                borderBottomColor: "white",
                                            },
                                            "&:hover:before": {
                                                borderBottomColor: "white",
                                            },
                                            "&.Mui-focused:before": {
                                                borderBottomColor: "green",
                                            },
                                            "&.Mui-focused:after": {
                                                borderBottomColor: "green",
                                            },
                                        },
                                    }}
                                />
                            </>
                        ) : (
                            <>
                                <Typography
                                    variant="h6"
                                    sx={{ color: "white" }}>
                                    {editedFirstName} {editedMiddleName}{" "}
                                    {editedLastName}
                                </Typography>
                                <Typography
                                    variant="body2"
                                    sx={{ color: grey[500] }}>
                                    {editedBirthDate}
                                </Typography>
                                <Typography
                                    variant="body2"
                                    sx={{ color: grey[500] }}>
                                    {editedPhoneNumber}
                                </Typography>
                            </>
                        )}
                    </Stack>
                </Box>
                <Divider />
                <Box
                    sx={{
                        p: 2,
                        display: "flex",
                        justifyContent: "space-between",
                        backgroundColor: grey[900]
                    }}>
                    <Typography
                        variant="button"
                        component="div"
                        sx={{ color: "white", marginTop: "10px" }}>
                        <Box
                            component="span"
                            fontWeight="fontWeightBold"
                            marginRight="8px">
                            Счет:
                        </Box>
                        {user.money}
                    </Typography>
                    <Button
                        onClick={handleOpen}
                        sx={{
                            "&:hover": {
                                backgroundColor: green[500],
                                color: "white",
                            },
                        }}>
                        Внести средства
                    </Button>
                </Box>
            </Card>
            <Dialog
                open={isModalOpen}
                onClose={handleClose}
                sx={{
                    backdropFilter: "blur(5px)",
                    "& .MuiPaper-root": {
                        borderRadius: 3,
                        bgcolor: "#132f4b",
                    },
                }}>
                <ReplenishForm close={handleClose} />
            </Dialog>
        </Box>
    );
};

export default ProfileForm;

