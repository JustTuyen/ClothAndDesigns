import men from '../assets/men.webp';
import women from '../assets/women.webp';
import accessories from '../assets/accessorie.webp'
import kid from '../assets/kid.webp';
//
import './css/CatSlider.css'

//
import ArrowOutwardOutlinedIcon from '@mui/icons-material/ArrowOutwardOutlined';
//
import Tooltip from '@mui/material/Tooltip';

//
import SlickModule from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
const Slider = SlickModule.default ?? SlickModule;

function CatCardSlider() {
    const settings = {
        infinite: true,
        slidesToShow: 5,
        slidesToScroll: 1,
        autoplay: true,
        speed: 2000,
        autoplaySpeed: 2000,
        pauseOnHover: true,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 4,
                },
            },
             {
                breakpoint: 700,
                settings: {
                    slidesToShow: 2,
                },
            },
        ],
    };

    return (
        <div className="w-full flex justify-center">
            <div className="slider-container w-3/4">
                <Slider {...settings}>
                    <div>
                        <img src={men} className="cat-items" alt="place holder man" />
                    </div>
                    <div>
                        <img src={women} className="cat-items" alt="place holder women" />
                    </div>
                    <div>
                        <img src={kid} className="cat-items" alt="place holder kid" />
                    </div>
                    <div>
                        <img src={accessories} className="cat-items" alt="place holder kid" />
                    </div>
                    <div>
                        <Tooltip title="Check more">
                            <div className="flex check-items
                            items-center justify-center border rounded-full
                            w-37.5 h-37.5 mx-auto">
                                <ArrowOutwardOutlinedIcon
                                    sx={{
                                        transition: "transform 0.3s ease",
                                        "&:hover": {
                                        transform: "rotate(45deg)",
                                        },
                                    }}
                                />
                            </div>
                        </Tooltip>
                    </div>
                </Slider>
            </div>
        </div>
    );
}
export default CatCardSlider;