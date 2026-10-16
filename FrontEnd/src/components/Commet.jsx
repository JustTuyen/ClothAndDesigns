//
import Kid from '../assets/Kid.webp'

//
import './css/Comment.css'
//
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import CardHeader from '@mui/material/CardHeader';
import IconButton from '@mui/material/IconButton';
import Avatar from '@mui/material/Avatar';
import Divider from "@mui/material/Divider";
import Stack from '@mui/material/Stack';
import Rating from '@mui/material/Rating';

//
import MoreVertIcon from '@mui/icons-material/MoreVert';
import PermIdentityIcon from '@mui/icons-material/PermIdentity';
//

function Commenṭ̣(){
    return(
        <>
        <div className="py-4">
            <div className="flex justify-between">
                <p className="text-[24px] font-bold">Comments</p>
                <div className="border rounded-full p-2">
                    <select>
                        <option>Thsi</option>
                    </select>
                </div>
            </div>
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4">
                <Card sx={{ width: "100%", maxWidth: 300 }}>
                    <CardHeader
                        avatar={
                            <Avatar sx={{ backgroundColor: "#00B7CD" }}>
                                <PermIdentityIcon />
                            </Avatar>
                        }
                        action={
                            <IconButton aria-label="settings">
                                <MoreVertIcon />
                            </IconButton>
                        }
                        title="Shrimp and Chorizo Paella"
                        subheader="September 14, 2016"
                    />

                    <Divider />

                    <CardContent>
                        <Stack spacing={2}>
                            <Stack direction="row" 
                            spacing={1}
                            sx={{
                                justifyContent: "flex-start",
                                alignItems: "center",
                            }}>
                                <Typography variant='body2'>Rate:</Typography>
                                <Rating name="half-rating" defaultValue={2.5} precision={0.5} />
                            </Stack>
                                    
                            <Typography variant="body2" sx={{ color: "text.secondary" }}>
                                This impressive paella is a perfect party dish and a fun meal
                                to cook together with your guests.
                            </Typography>

                            <CardMedia
                                className="img-comment"
                                component="img"
                                image={Kid}
                                alt="Comment image"
                            />
                        </Stack>
                    </CardContent>
                </Card>
            </div>
        </div>
        </>
    )
}

export default Commenṭ̣;