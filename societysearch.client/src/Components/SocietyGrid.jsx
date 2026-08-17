import ImageList from "@mui/material/ImageList";
import ImageListItem from "@mui/material/ImageListItem";
import ImageListItemBar from "@mui/material/ImageListItemBar";
import ListSubheader from "@mui/material/ListSubheader";
import IconButton from "@mui/material/IconButton";
import InfoIcon from "@mui/icons-material/Info";
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import Radio from "@mui/material/Radio";
import FormControlLabel from "@mui/material/FormControlLabel";
import Button from "@mui/material/Button";
import React from "react";
import { RadioGroup } from "@mui/material";
import AddSociety from "./AddSociety";
import EditSociety from "./EditSociety";
import SocietyDetails from "./SocietyDetails";

export default function SocietyGrid({ onDetailsViewChange, isManager = false }) {
    const [addSocietyOpen, setAddSocietyOpen] = React.useState(false);
    const [editSocietyOpen, setEditSocietyOpen] = React.useState(false);
    const [selectedSociety, setSelectedSociety] = React.useState(null);
    const [detailsSociety, setDetailsSociety] = React.useState(null);

    const handleEditClick = (society) => {
        setSelectedSociety(society);
        setEditSocietyOpen(true);
    };

    const showSocietyDetails = (society) => {
        setDetailsSociety(society);
        onDetailsViewChange?.(true);
    };

    const hideSocietyDetails = () => {
        setDetailsSociety(null);
        onDetailsViewChange?.(false);
    };

    if (detailsSociety) {
        return (
            <SocietyDetails
                society={detailsSociety}
                onBack={hideSocietyDetails}
            />
        );
    }

    return (
        <React.Fragment>
            <ListSubheader
                component="div"
                style={{
                    position: "relative",
                    fontSize: "20px",
                    fontWeight: "bold",
                    color: "#111",
                    textAlign: "left",
                    marginTop: "75px",
                    display: "flex",
                    flexDirection: "row",
                    alignItems: "center",
                    gap: "50px",
                }}
            >
                Unit Availability:
                <RadioGroup
                    row
                    aria-label="units"
                    name="row-radio-buttons-group"
                    defaultValue="available"
                    sx={{ display: "flex", flexDirection: "row", gap: "20px", alignItems: "center" }}
                >
                    <FormControlLabel 
                        value="available" 
                        control={<Radio color="success" />} 
                        label="Available" 
                    />
                    <FormControlLabel 
                        value="not_available" 
                        control={<Radio color="error" />} 
                        label="Occupied" 
                    />
                </RadioGroup>
                {isManager && (
                    <Button 
                        variant="contained" 
                        color="primary" 
                        startIcon={<AddIcon />}
                        sx={{ marginLeft: "875px" }}
                        onClick={() => setAddSocietyOpen(true)}
                    >
                        Add Society
                    </Button>
                )}
            </ListSubheader>

            <ImageList
                style={{ marginTop: "15px" }}
                sx={{ width: "100%", height: "100vh" }}
            >
                {itemData.map((item) => (
                    <ImageListItem key={item.img}>
                        <img
                            srcSet={`${item.img}?w=248&fit=crop&auto=format&dpr=2 2x`}
                            src={`${item.img}?w=248&fit=crop&auto=format`}
                            alt={item.title}
                            loading="lazy"
                        />
                        <ImageListItemBar
                            sx={{
                                height:"200px",
                                textAlign:"left",
                            }}
                            title={item.title}
                            subtitle={item.author}
                            actionIcon={
                                <div style={{ display: "flex", gap: "5px" }}>
                                    {isManager && (
                                        <>
                                            <IconButton
                                                aria-label={`delete ${item.title}`}
                                                color="error"
                                            >
                                                <DeleteIcon sx={{ fontSize: "35px" }} />
                                            </IconButton>
                                            <IconButton
                                                aria-label={`edit ${item.title}`}
                                                color="primary"
                                                onClick={() => handleEditClick(item)}
                                            >
                                                <EditIcon sx={{ fontSize: "35px" }} />
                                            </IconButton>
                                        </>
                                    )}
                                    <IconButton
                                        // sx={{ color: "rgba(255, 255, 255, 0.54)" }}
                                        color="secondary"
                                        aria-label={`info about ${item.title}`}
                                        onClick={() => showSocietyDetails(item)}
                                    >
                                        <InfoIcon sx={{ fontSize: "35px" }} />
                                    </IconButton>
                                    
                                </div>
                            }
                        />
                    </ImageListItem>
                ))}
            </ImageList>
            <AddSociety
                open={addSocietyOpen}
                onClose={() => setAddSocietyOpen(false)}
            />
            <EditSociety
                key={selectedSociety?.img ?? "edit-society"}
                open={editSocietyOpen}
                onClose={() => setEditSocietyOpen(false)}
                initialSociety={selectedSociety}
                onSave={() => setEditSocietyOpen(false)}
            />
        </React.Fragment>
    );
}

const itemData = [
    {
        img: "https://images.unsplash.com/photo-1551963831-b3b1ca40c98e",
        title: "Breakfast",
        author: "@bkristastucchio",
        rows: 2,
        cols: 2,
        featured: true,
    },
    {
        img: "https://images.unsplash.com/photo-1551782450-a2132b4ba21d",
        title: "Burger",
        author: "@rollelflex_graphy726",
    },
    {
        img: "https://images.unsplash.com/photo-1522770179533-24471fcdba45",
        title: "Camera",
        author: "@helloimnik",
    },
    {
        img: "https://images.unsplash.com/photo-1444418776041-9c7e33cc5a9c",
        title: "Coffee",
        author: "@nolanissac",
        cols: 2,
    },
    {
        img: "https://images.unsplash.com/photo-1533827432537-70133748f5c8",
        title: "Hats",
        author: "@hjrc33",
        cols: 2,
    },
    {
        img: "https://images.unsplash.com/photo-1558642452-9d2a7deb7f62",
        title: "Honey",
        author: "@arwinneil",
        rows: 2,
        cols: 2,
        featured: true,
    },
    {
        img: "https://images.unsplash.com/photo-1516802273409-68526ee1bdd6",
        title: "Basketball",
        author: "@tjdragotta",
    },
    {
        img: "https://images.unsplash.com/photo-1518756131217-31eb79b20e8f",
        title: "Fern",
        author: "@katie_wasserman",
    },
    {
        img: "https://images.unsplash.com/photo-1597645587822-e99fa5d45d25",
        title: "Mushrooms",
        author: "@silverdalex",
        rows: 2,
        cols: 2,
    },
    {
        img: "https://images.unsplash.com/photo-1567306301408-9b74779a11af",
        title: "Tomato basil",
        author: "@shelleypauls",
    },
    {
        img: "https://images.unsplash.com/photo-1471357674240-e1a485acb3e1",
        title: "Sea star",
        author: "@peterlaster",
    },
    {
        img: "https://images.unsplash.com/photo-1589118949245-7d38baf380d6",
        title: "Bike",
        author: "@southside_customs",
        cols: 2,
    },
];
