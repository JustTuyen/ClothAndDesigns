import * as React from 'react';
//
import ImgIcon from '../assets/Icon.png'
//
import Stack from '@mui/material/Stack';
import IconButton from '@mui/material/IconButton';
import Badge, { badgeClasses } from '@mui/material/Badge';
import { styled } from '@mui/material/styles';
import Tooltip from '@mui/material/Tooltip';
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import Button from '@mui/material/Button';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';

//
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MailIcon from '@mui/icons-material/Mail';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import SearchOutlinedIcon from '@mui/icons-material/SearchOutlined';
import AccountCircleOutlinedIcon from '@mui/icons-material/AccountCircleOutlined';
import ShoppingBasketOutlinedIcon from '@mui/icons-material/ShoppingBasketOutlined';
import MenuIcon from '@mui/icons-material/Menu';
//
const CartBadge = styled(Badge)`
  & .${badgeClasses.badge} {
    top: -12px;
    right: -6px;
  }
`;



function NavBar(){

    //drawer
    const [openDrawer, setOpenDrawer] = React.useState(false);

    const toggleDrawer = (newOpen) => () => {
        setOpenDrawer(newOpen);
    };
    const DrawerList = (
        <Box sx={{ width: 250 }} role="presentation" onClick={toggleDrawer(false)}>
            <List>
                {['Inbox', 'Starred', 'Send email', 'Drafts'].map((text, index) => (
                <ListItem key={text} disablePadding>
                    <ListItemButton>
                    <ListItemIcon>
                        {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
                    </ListItemIcon>
                    <ListItemText primary={text} />
                    </ListItemButton>
                </ListItem>
                ))}
            </List>
            <Divider />
            <List>
                {['All mail', 'Trash', 'Spam'].map((text, index) => (
                <ListItem key={text} disablePadding>
                    <ListItemButton>
                    <ListItemIcon>
                        {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
                    </ListItemIcon>
                    <ListItemText primary={text} />
                    </ListItemButton>
                </ListItem>
                ))}
            </List>
        </Box>
    );
    //
    const id = React.useId();
    const buttonId = `${id}-button`;
    const menuId = `${id}-menu`;

    //profile btn
    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);
    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };



    return (
        <>
        <nav className="top-0 p-2 bg-[#23A393]">
            <div className="flex justify-between items-center">
                <div className="p-2 block lg:hidden">
                    <IconButton aria-label="drawer" onClick={toggleDrawer(true)}>
                        <MenuIcon />
                    </IconButton>
                    <Drawer open={openDrawer} onClose={toggleDrawer(false)}>
                        {DrawerList}
                    </Drawer>
                </div>
                <div className="p-2">
                    <img src={ImgIcon} alt="wed-logo" />
                </div>
                <div className="p-2">
                    <Stack direction="row" spacing={1} sx={{ alignItems: 'center' }}>
                        <Tooltip title="Click to search">
                            <IconButton aria-label="search" sx={{color:'white'}}>
                                <SearchOutlinedIcon />
                            </IconButton>
                        </Tooltip>
                        <Tooltip title="Click to cart">
                            <IconButton aria-label="cart">
                                <ShoppingBasketOutlinedIcon  sx={{color:'white'}} fontSize="small" />
                                <CartBadge badgeContent={2} 
                                color="error"
                                overlap="circular" />
                            </IconButton>
                        </Tooltip>
                        <Tooltip title="Click to see profile">
                            <IconButton aria-label="user" sx={{color:'white'}}
                                id={buttonId}
                                aria-controls={open ? menuId : undefined}
                                aria-haspopup="true"
                                aria-expanded={open}
                                onClick={handleClick}
                            >
                                <AccountCircleOutlinedIcon />
                            </IconButton>
                            <Menu
                                id={menuId}
                                anchorEl={anchorEl}
                                open={open}
                                onClose={handleClose}
                                slotProps={{
                                list: {
                                    'aria-labelledby': buttonId,
                                },
                                }}
                            >
                                <MenuItem onClick={handleClose}>Profile</MenuItem>
                                <MenuItem onClick={handleClose}>My account</MenuItem>
                                <MenuItem onClick={handleClose}>Logout</MenuItem>
                            </Menu>
                        </Tooltip>
                    </Stack>
                </div>

            </div>
            {/* categories and subcats */}
            <div className="hidden lg:flex justify-center">
                <div className="items-center gap-2 flex-wrap">
                    <Button
                        sx={{
                            fontSize: '16px',
                            color: 'white',
                            textTransform: 'none',
                        }}
                        endIcon={<ExpandMoreIcon />}
                    >
                        Category
                    </Button>   
                </div>   
            </div>
        </nav>
        </>
    )
}

export default NavBar;