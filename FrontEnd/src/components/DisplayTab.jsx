
import * as React from "react";

//
import './css/DisplayTab.css'
//
function NewArrival(){
    return(
        <>
        <p>New arrival</p>
        </>
    )
}

function BestDeal(){
    return(
        <>
        <p>best deal</p>
        </>
    )
}

function HotProduct(){
    return(
        <>
        <p>Hot</p>
        </>
    )
}


function DisplayTab(){
    const [activeTab, setActiveTab] = React.useState('hotProduct');
    const renderSubView = () => {
        switch(activeTab){
            case 'newArrival':
                return <NewArrival/>
            case 'bestDeal':
                return <BestDeal/>
            case 'hotProduct':
                return <HotProduct/>
        }
    }

    return(
        <>
        <div className="p-4">
            <div className="flex justify-start gap-4 text-lg
            md:justify-center md:text-3xl md:gap-8
            ">
                <span
                onClick={() => setActiveTab('newArrival')}
                className={`hover-line1 tab ${
                    activeTab === 'newArrival'
                    ? 'font-extrabold'
                    : 'font-light'
                }`}>
                    Newly Arrival
                </span>
                
                <span
                onClick={() => setActiveTab('bestDeal')}
                className={`hover-line2 tab ${
                    activeTab === 'bestDeal'
                    ? 'font-bold'
                    : 'font-light'
                }`}>
                    Best Deal
                </span>
                <span
                onClick={() => setActiveTab('hotProduct')}
                className={`hover-line3 tab ${
                    activeTab === 'hotProduct'
                    ? 'font-bold'
                    : 'font-light'
                }`}>
                    Hot Product
                </span>
            </div>
            <div className="">
                {renderSubView()}
            </div>
        </div>
        </>
    )
}

export default DisplayTab;