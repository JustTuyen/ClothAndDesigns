
import * as React from "react";
//
import Divider from '@mui/material/Divider';

//
import './css/DisplayTab.css'
//
function Description(){
    return(
        <>
        <div className="p-2">
            <p>Description</p>
        </div>
        </>
    )
}

function Review(){
    return(
        <>
        <p>Review</p>
        </>
    )
}

function Policy(){
    return(
        <>
        <p>Return Policy</p>
        </>
    )
}

function SizeHelper(){
    return(
        <>
        <p>help size</p>
        </>
    )
}

function DetailTab(){
    const [activeTab, setActiveTab] = React.useState('description');
    const renderSubView = () => {
        switch(activeTab){
            case 'description':
                return <Description/>
            case 'review':
                return <Review/>
            case 'policy':
                return <Policy/>
            case 'sizing':
                return <SizeHelper/>
        }
    }

    return(
        <>
        <div className="">
            <div className="flex justify-start gap-4
            md:justify-center md:gap-8
            ">
                <span
                onClick={() => setActiveTab('description')}
                className={`tab-title tab01 ${
                    activeTab === 'description'
                    ? 'font-extrabold'
                    : 'font-light'
                }`}>
                    Description
                </span>
                
                <span
                onClick={() => setActiveTab('review')}
                className={`tab-title tab02 ${
                    activeTab === 'review'
                    ? 'font-bold'
                    : 'font-light'
                }`}>
                    Reviews
                </span>
                <span
                onClick={() => setActiveTab('policy')}
                className={`tab-title tab03 ${
                    activeTab === 'policy'
                    ? 'font-bold'
                    : 'font-light'
                }`}>
                    Policy
                </span>
                <span
                onClick={() => setActiveTab('sizing')}
                className={`tab-title tab04 ${
                    activeTab === 'sizing'
                    ? 'font-bold'
                    : 'font-light'
                }`}>
                    Size Guide
                </span>
            </div>
            <Divider variant="middle"/>
            <div className="">
                {renderSubView()}
            </div>
        </div>
        </>
    )
}

export default DetailTab;