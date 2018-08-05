import * as moment from 'moment';
const updateFieldType = 'UPDATE_FIELD';
const loginRequested = 'LOGIN_REQUESTED';
const loginSucceeded = 'LOGIN_SUCCEEDED';
const loginFailed = 'LOGIN_FAILED';
const initialState = {
    list: [{
        id: '1234',
        firstName: 'Daric',
        lastName: 'Teske',
        birthday: moment('12/12/1995'),
        tShirtSize: 'XXL',
    },
    {
        id: '2345',
        firstName: 'asdf',
        lastName: 'asdf',
        birthday: moment('12/12/1997'),
        tShirtSize: 'XL',
    },
    {
        id: '3456',
        firstName: 'qwer',
        lastName: 'qwer',
        birthday: moment('12/12/1996'),
        tShirtSize: 'L',
    }]
}

export const actionCreators = {
    // updateField: (name, value) => (dispatch) => {
    //     dispatch({ type: updateFieldType, name, value });
    // },
    // login: () => async (dispatch, getState) => {
    //     const enteredInfo = getState().userStore;
    //     dispatch({ type: loginRequested });
    //     return fetch(`/api/auth/login`, {
    //         method: "POST",
    //         headers: {
    //             'Content-Type': 'application/json'
    //         },
    //         body: JSON.stringify({ username: enteredInfo.username, password: enteredInfo.password }),
    //     })
    //         .then(response => response.json())
    //         .then(data => {
    //             localStorage.setItem("token", data.token);
    //             dispatch({ type: loginSucceeded, data });
    //         })
    //         .catch(err => dispatch({ type: loginFailed, err }));
    // }
};

export const reducer = (state, action) => {
    state = state || initialState;

    // if (action.type === updateFieldType) {
    //     return {
    //         ...state,
    //         [action.name]: action.value
    //     }
    // }

    return state;
};
