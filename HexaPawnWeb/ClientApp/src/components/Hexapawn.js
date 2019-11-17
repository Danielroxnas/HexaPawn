import React, { useState, useEffect } from 'react';

export const HexaPawn = (props) => {

    const [piece, setPiece] = useState([]);
    const [loading, setLoading] = useState(true);
    const [actions, setActions] = useState([]);
    const [winning, setWinning] = useState(false);
    var d = [{}];
    const populatData = async () => {
        const response = await fetch('api/games/GameBoard');
        const data = await response.json();
        d = data;
        setPiece(d.Pieces1);
        console.log(d.Pieces1);
        //d = data;
        //const response2 = await fetch('api/games/GetPieces', {
        //    method: 'POST',
        //    body: JSON.stringify(data),
        //    headers: {
        //        'Content-Type': 'application/json',
        //    }
        //})
        //const data2 = await response2.json();

        setLoading(false);
        //var d = JSON.stringify(data);
        //generateAction(d);

    };

    const boardAction = { d, ...actions }
    const makeAction = async (action) => {
        setActions(action);
        const response = await fetch('api/games/makeaction', {
            method: 'POST',
            body: JSON.stringify(boardAction),
            headers: {
                'Content-Type': 'application/json',
            }
        })
        //const data = await response.json();
        //setBoard(data);
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
        <div>
            <div className="row">
                <div>{pieces["A1"]}</div>
                <div>{pieces["A2"]}</div>
                <div>{pieces["A3"]}</div>
            </div>
            <div className="row">
                <div>{pieces["B1"]}</div>
                <div>{pieces["B2"]}</div>
                <div>{pieces["B3"]}</div>

            </div>
            <div className="row">
                <div>{pieces["C1"]}</div>
                <div>{pieces["C2"]}</div>
                <div>{pieces["C3"]}</div>

            </div>
        </div>

    );
}