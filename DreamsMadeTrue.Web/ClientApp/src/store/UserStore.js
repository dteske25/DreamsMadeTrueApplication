const updateFieldType = 'UPDATE_FIELD';
const invalidFormValues = 'INVALID_FORM_VALUES';

const loginRequested = 'LOGIN_REQUESTED';
const loginSucceeded = 'LOGIN_SUCCEEDED';
const loginFailed = 'LOGIN_FAILED';

const registrationRequested = 'REGISTRATION_REQUESTED';
const registrationSucceeded = 'REGISTRATION_SUCCEEDED';
const registrationFailed = 'REGISTRATION_FAILED';

const logoutUser = 'LOGOUT_USER';

const initialState = {
  username: '',
  email: '',
  firstName: '',
  lastName: '',
  password: '',
  confirmPassword: '',
  token: '',
  userInfo: {},
  loading: false,
  submittedOnce: false,
}

export const actionCreators = {
  updateField: (name, value) => (dispatch) => {
    dispatch({ type: updateFieldType, name, value });
  },
  login: () => async (dispatch, getState) => {
    const enteredInfo = getState().userStore;
    dispatch({ type: loginRequested });
    return fetch('/api/auth/login', {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ username: enteredInfo.username, password: enteredInfo.password }),
    })
      .then(response => response.json())
      .then(data => {
        localStorage.setItem("token", data.token);
        localStorage.setItem("userInfo", data.userInfo);
        dispatch({ type: loginSucceeded, data });
      })
      .catch(err => dispatch({ type: loginFailed, err }));
  },
  register: () => async (dispatch, getState) => {
    const enteredInfo = getState().userStore;
    if (enteredInfo.password !== enteredInfo.confirmPassword
      || enteredInfo.username === ''
      || enteredInfo.email === ''
      || enteredInfo.firstName === ''
      || enteredInfo.lastName === '') {
      dispatch({ type: invalidFormValues });
      return;
    }

    dispatch({ type: registrationRequested });
    return fetch('/api/auth/register', {
      method: "POST",
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        username: enteredInfo.username,
        password: enteredInfo.password,
        firstName: enteredInfo.firstName,
        lastName: enteredInfo.lastName,
        email: enteredInfo.email,
      }),
    })
      .then(response => response.json())
      .then(data => {
        localStorage.setItem("token", data.token);
        localStorage.setItem("userInfo", data.userInfo);
        dispatch({ type: registrationSucceeded, data });
      })
      .catch(err => dispatch({ type: registrationFailed, err }));
  },
  logout: () => async (dispatch, getState) => {
    localStorage.clear();
    dispatch({ type: logoutUser });
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

  if (state.token === '') {
    state.token = localStorage.getItem("token");
    state.userInfo = localStorage.getItem("userInfo");
  }


  if (action.type === updateFieldType) {
    return {
      ...state,
      [action.name]: action.value
    }
  }

  if (action.type === invalidFormValues) {
    return {
      ...state,
      submittedOnce: true
    };
  }

  if (action.type === loginRequested || action.type === registrationRequested) {
    return {
      ...state,
      loading: true
    };
  }

  if (action.type === loginSucceeded || action.type === registrationSucceeded) {
    return {
      ...state,
      loading: false,
      username: '',
      password: '',
      token: action.data.token,
      userInfo: action.data.userInfo
    };
  }

  if (action.type === logoutUser) {
    return {
      ...state,
      token: '',
      userInfo: ''
    };
  }

  return state;
};
