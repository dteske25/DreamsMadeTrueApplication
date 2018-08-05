const updateFieldType = 'UPDATE_FIELD';
const loginRequested = 'LOGIN_REQUESTED';
const loginSucceeded = 'LOGIN_SUCCEEDED';
const loginFailed = 'LOGIN_FAILED';
const initialState = {
    username: '',
    password: '', 
    token: '',
    userInfo: {}
}

export const actionCreators = {
    updateField: (name, value) => (dispatch) => {
        dispatch({ type: updateFieldType, name, value });
    },
    login: () => async (dispatch, getState) => {
        const enteredInfo = getState().userStore;
        dispatch({ type: loginRequested });
        return fetch(`/api/auth/login`, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ username: enteredInfo.username, password: enteredInfo.password }),
        })
            .then(response => response.json())
            .then(data => {
                localStorage.setItem("token", data.token);
                dispatch({ type: loginSucceeded, data });
            })
            .catch(err => dispatch({ type: loginFailed, err }));
    }
    //requestWeatherForecasts: startDateIndex => async (dispatch, getState) => {
    //    if (startDateIndex === getState().weatherForecasts.startDateIndex) {
    //        // Don't issue a duplicate request (we already have or are loading the requested data)
    //        return;
    //    }

    //    dispatch({ type: requestWeatherForecastsType, startDateIndex });

    //    const url = `api/SampleData/WeatherForecasts?startDateIndex=${startDateIndex}`;
    //    const response = await fetch(url);
    //    const forecasts = await response.json();

    //    dispatch({ type: receiveWeatherForecastsType, startDateIndex, forecasts });
    //}
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === updateFieldType) {
        return {
            ...state,
            [action.name]: action.value
        }
    }

    if (action.type === loginSucceeded) {
        return {
            ...state,
            username: '',
            password: '',
            token: action.data.token,
            userInfo: action.data.userInfo
        }
    }

    return state;
};
