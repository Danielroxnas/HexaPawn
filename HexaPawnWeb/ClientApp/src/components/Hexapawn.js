import React, { useState, useEffect } from 'react';

export const HexaPawn = (props) => {

    const [board, setBoard] = useState([]);
    const [loading, setLoading] = useState(true);
    const [actions, setActions] = useState([]);
    const [winning, setWinning] = useState(false);

    const populatData = async () => {
        const response = await fetch('api/games/GameBoard');
        const data = await response.json();
        setBoard(data);
        setLoading(false);
        console.log(data);
        var d = JSON.stringify(data);
        generateAction(d);

    };  

    const boardAction = {...board, ...actions}
    const makeAction = async (action) => {
        setActions(action);
        const response = await fetch('api/games/makeaction', {
            method: 'POST',
            body: JSON.stringify(boardAction),
            headers: {
                'Content-Type': 'application/json',
            }
        })
        const data = await response.json();
        setBoard(data);
        generateAction();
    }

    const generateAction = async (fetchdata) => {
        const response = await fetch("api/games/getActions", {
            method: 'POST',
            body: fetchdata,
            headers: {
                'Content-Type': 'application/json',
            }
        })
        const data = await response.json();
        setActions(data);
    }
    useEffect(() => {
        populatData();

    }, [])

    let contents = loading
        ? <p><em>Loading...</em></p>
        : renderHexapawnTable(board.Pieces);
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
    return (
            //{props.actions.map((a, i) => {
            //    return (
            //        <button
            //            key={i}
            //            onClick={() => props.sa(a)}>
            //            {a.action}
            //        </button>
            //    );
            //})}
        <div>
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
