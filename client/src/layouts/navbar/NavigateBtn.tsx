import { Button } from "@mui/material";
import { green } from "@mui/material/colors";
import { Link } from "react-router-dom";


const NavigateBtn = ({ to, body }: { to: string; body: string }) => {
    return (
        <Button
            component={Link}
            to={to}
            sx={{
                "&:hover": {
                    background: green[800],
                },
            }}>
            {body}
        </Button>
    );
};

export default NavigateBtn;