import * as React from "react";
import AppBar from "@mui/material/AppBar";
import SearchIcon from "@mui/icons-material/Search";
import LogoutIcon from "@mui/icons-material/Logout";
import Logo from "../assets/Logo.png";
import { InputBase, styled, alpha } from "@mui/material";
import Button from "@mui/material/Button";
import Login from "./Login";

const Search = styled("div")(({ theme }) => ({
    position: "relative",
    borderRadius: theme.shape.borderRadius,
    backgroundColor: alpha(theme.palette.common.white, 0.15),
    "&:hover": {
        backgroundColor: alpha(theme.palette.common.white, 0.25),
    },
    marginRight: theme.spacing(2),
    marginLeft: 0,
    width: "100%",
    [theme.breakpoints.up("sm")]: {
        marginLeft: theme.spacing(3),
        width: "auto",
    },
    left: "180px",
    border: "1px solid #ccc",
    height: "50px",
    marginTop: "10px",
}));

const SearchIconWrapper = styled("div")(({ theme }) => ({
    padding: theme.spacing(0, 2),
    height: "100%",
    position: "absolute",
    pointerEvents: "none",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
}));

const StyledInputBase = styled(InputBase)(({ theme }) => ({
    color: "inherit",
    "& .MuiInputBase-input": {
        padding: theme.spacing(1, 1, 1, 0),
        // vertical padding + font size from searchIcon
        paddingLeft: `calc(1em + ${theme.spacing(4)})`,
        transition: theme.transitions.create("width"),
        width: "100%",
        [theme.breakpoints.up("md")]: {
            width: "76ch",
            marginTop: "13px",
        },
    },
}));

export default function HeaderAppBar({ hideSearch = false, isManager = false, onManagerLogin, onLogout, onLogoClick }) {
    const [loginOpen, setLoginOpen] = React.useState(false);

    return (
        <React.Fragment>
            <AppBar
                style={{
                    backgroundColor: "#fff",
                    color: "#111",
                    display: "flex",
                    flexDirection: "row",
                    flexWrap: "wrap",
                    alignContent: "center",
                }}
            >
                <button
                    type="button"
                    aria-label="Go to main page"
                    onClick={onLogoClick}
                    style={{ background: "none", border: 0, padding: 0, cursor: "pointer" }}
                >
                    <img
                        src={Logo}
                        alt="Logo"
                        style={{ height: "65px", width: "65px" }}
                    />
                </button>
                {!hideSearch && (
                    <Search>
                        <SearchIconWrapper>
                            <SearchIcon />
                        </SearchIconWrapper>
                        <StyledInputBase
                            placeholder="Search…"
                            inputProps={{ "aria-label": "search" }}
                        />
                    </Search>
                )}
                {isManager ? (
                    <Button
                        variant="contained"
                        color="primary"
                        startIcon={<LogoutIcon />}
                        style={{ marginLeft: "auto", marginRight: "50px", height: "40px", width: "150px", marginTop: "10px" }}
                        onClick={onLogout}
                    >
                        Logout
                    </Button>
                ) : (
                    <Button
                        variant="contained"
                        color="primary"
                        style={{ marginLeft: "auto", marginRight: "50px", height: "40px", width: "150px", marginTop: "10px" }}
                        onClick={() => setLoginOpen(true)}
                    >
                        Manager Login
                    </Button>
                )}
            </AppBar>
            <Login
                open={loginOpen}
                onClose={() => setLoginOpen(false)}
                onLogin={onManagerLogin}
            />
        </React.Fragment>
    );
}
