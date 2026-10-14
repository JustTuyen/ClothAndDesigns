
import banner01 from '../assets/banner01.webp'
import banner02 from '../assets/banner02.webp'
//

import SlickModule from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
const Slider = SlickModule.default ?? SlickModule;
//
import './css/Banner.css'
//

function Banner(){
    const settings = {
        dots: false,
        infinite: true,
        fade: true,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 2000,
        pauseOnHover: true,
    };

    return (
        <>
        <div className="slider-container">
            <Slider {...settings}>
                <div>
                    <img src={banner01} className='banner-items' alt="place holder banner 01" />
                </div>
                <div>
                    <img src={banner02} className='banner-items'  alt="place holder banner 02" />
                </div>
                <div>
                    <img src={banner01} className='banner-items'  alt="place holder banner 01" />
                </div>
                <div>
                    <img src={banner02} className='banner-items'  alt="place holder banner 02" />
                </div>                
            </Slider>
        </div>
        </>
    )
}

export default Banner;