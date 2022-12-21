import React from 'react';
import "./style.css";
const CustomBtn = ({disabled, text, size, background, width, selectedIcon, clicked}) => {

function clickedOnBtn() {
    clicked(true);
}

    return(
        <button disabled={disabled} className={`general ${size} ${background}`} style={{width: `${width}`}} onClick={clickedOnBtn}>
            {text}
            {
                disabled ? null : selectedIcon && (<img src={selectedIcon} alt="" width={'30px'} className={'ms-2'}/>)
            }
        </button>
    )
}
export default CustomBtn;
