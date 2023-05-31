import { Box } from "@mui/material";
import { useEffect, useState } from "react";
import { CreateProductForm } from "../../components";
import { IProduct, IUser } from "../../shared";
import { getAllUsers } from "../../lib/users/users";
import { getCookie } from "typescript-cookie";
import { UserListComponent } from "../../components/userlist";
import { CreateUserForm } from "../../components/forms/CreateUserForm";
import { green } from "@mui/material/colors";

const createUser = (
    id: string,
    birthDate: string,
    firstName: string,
    image: string,
    lastName: string,
    middleName: string,
    money: number,
    number: string,
    orders: IProduct[]
): IUser => {
    return {
        id,
        birthDate,
        firstName,
        image,
        lastName,
        middleName,
        money,
        number,
        orders,
        gender: "male"
    };
};

const createUsers = (count: number) => {
    // return new Array(count).fill(null).map((_, i) => {
    //     return createUser(
    //         `${i}`,
    //         `01.01.000${i}`,
    //         `Name${i}`,
    //         "https://loremflickr.com/640/360",
    //         `lastName${i}`,
    //         `middleName${i}`,
    //         1000 + i * 100,
    //         `number${i}`,
    //         []
    //     );
    // });

    return [
        createUser("0", "03.04.1987", "Иван", "", "Иванович", "Иванов", 2000, "8 999 111 22 33", []),
        createUser("1", "05.04.1987", "Сергей", "", "Сергеевич", "Сергеев", 2000, "8 999 111 22 34", []),
    ]
};

const AdminPage = () => {
    const [users, setUsers] = useState<IUser[]>(createUsers(5));

    useEffect(() => {
        fetchUsers();
    },[]);

    const fetchUsers = async () => {
        try {
            const { data } = await getAllUsers(
                getCookie("jwt-authorization") ?? ""
            );
            setUsers(data.users);
        } catch (error) {
        }
    };

    return (
        <Box
            bgcolor={green[900]}
            display={"flex"}
            alignItems={"center"}
            height={"100vh"}
            width={"100vw"}>
            <Box
                display={"flex"}
                flexDirection={"column"}
                justifyContent={"space-evenly"}
                alignItems={"center"}
                width={"80%"}
                marginLeft={"auto"}
                marginRight={"auto"}>
                <UserListComponent users={users}/>
                <Box marginTop={"10px"}>
                    <CreateProductForm />
                    <CreateUserForm onCloseCallback={fetchUsers}/>
                </Box>
            </Box>
        </Box>
    );
};

export default AdminPage;
