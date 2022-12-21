import React, {useEffect, useState} from 'react';
import "./style.css";
import clock from "../../assets/gifs/clock.gif";
const Counter = ({limit, isOver}) => {
    const [count, setCount] = useState(limit);

    useEffect(() => {
        if (count > 0) {
            setTimeout(() => setCount(count - 1), 1000);
        } else {
            isOver(true);
            setCount(0);
        }
    });

    return(
        <div>
            <img src={clock} alt="" width={'60px'}/>
            <span>{count}</span>
        </div>
    )
}
export default Counter;
