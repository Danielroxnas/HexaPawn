import React, { useState, useEffect } from 'react';

export const HexaPawn = (props) => {

    const [piece, setPiece] = useState([]);
    const [loading, setLoading] = useState(true);
    const [actions, setActions] = useState([]);
    const [winning, setWinning] = useState(false);

    const populatData = async () => {
        const response = await fetch('api/games/GameBoard');
        const data = await response.json();
        setPiece(data);
        setLoading(false);
        console.log(data);
    };



    const makeAction = async (action) => {
        const response = await fetch('api/games/makeAction/', {
        body: JSON.stringify(action)});
        const data = await response.json();
        setWinning(data);
        //response = await fetch('api/games/getActions');
        //data = await response.json();
        //setAction(data);
    };
    // Detta blir som en componentDidMount
 
   

    const generateAction = async () => {
        const response = await fetch("api/games/getActions");
        const data = await response.json();
        setActions(data);
    }
    useEffect(() => {
        generateAction();
    }, [])

    useEffect(() => {
        populatData();
    }, []) 

    let contents = loading
        ? <p><em>Loading...</em></p>
        : renderHexapawnTable(piece);
    return (
        <div>
            <h1 id="tabelLabel" >Hexa pawn</h1>
            <p></p>
            {contents}
            <Actions
                sa={makeAction}
                action={actions} />
        </div>
    );

}
const Actions = props => {
    //console.log(props);
    const handleClick = (a) => {
        console.log(a);
    };
    return (
        <div>
            {props.action.map((a, i) => {
                return (
                    <button
                        key={i}
                        onClick={() => props.sa(a)}>
                        {a.action}
                    </button>
                );
            })}
        </div>
    );
}

const renderHexapawnTable = (pieces) => {
    const colors = {
        1: '#C6C6C6',
        2: '#000'
    };

    const getColor = num => {
        return colors[num] || 'transparent';
    };
    return (
        <table className="table" aria-labelledby="tabelLabel">
            <tbody style={{ border: '1px solid black', textAlign: 'center' }}>
                {pieces.map((piece, i) =>
                    <tr style={{ border: '1px solid black', textAlign: 'center' }} key={i}>
                        {piece.map((item, i) =>
                            <td style={{ border: '1px solid black', textAlign: 'center' }} key={i}>
                                <span style={{ color: getColor(item) }}>{item}</span>
                            </td>
                        )}
                    </tr>
                )}
            </tbody>
        </table>
    );
}
