import { Box, Container } from "@mui/material";
import { Document } from "../forms/DocsForm";
import { IDocument } from "../../shared/models/docs";
import { green } from "@mui/material/colors";

interface ProductContainerProps {
    documents: IDocument[];
}

const DocumentsList = ({ documents }: ProductContainerProps) => {
    return (
        <Container
            sx={{
                display: "flex",
                justifyContent: "center",
                flexDirection: "column",
                rowGap: "10px",
            }}>
            {documents.map((document, i) => (
                <Box
                    key={i}
                    height={"100%"}
                    sx={{
                        display: "flex",
                        flexDirection: "column",
                        justifyContent: "space-between",
                        backgroundColor: green[600],
                        padding: "10px",
                        borderRadius: "4px",
                        boxShadow: "0 2px 4px rgba(0, 0, 0, 0.2)",
                    }}>
                    <Document
                        filename={document.filename}
                        link={document.link}
                    />
                </Box>
            ))}
        </Container>
    );
};

export default DocumentsList;

