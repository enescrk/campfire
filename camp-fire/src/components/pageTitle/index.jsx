import React, {useEffect, useState} from 'react';
import "./style.css";

const PageTitle = ({title}) => {

    const {header, setHeader} = useState(title);

    return (<div className={'title'}>
            <div className={'mx-auto w-50'}>
                <div>{title}</div>
                <div>/w Artifical Intelligance</div>
                <div className={'line mt-2 w-75 mx-auto'}></div>
            </div>
        </div>
    )
}

export default PageTitle;
