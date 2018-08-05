import React, { Component } from 'react';
import { Route, Redirect } from 'react-router';
import { connect } from 'react-redux';
import Layout from './components/Layout';
import Home from './components/Home';
import Login from './components/Login';
import Register from './components/Register';
import ContestantList from './components/ContestantList';
import ContestantProfile from './components/ContestantProfile';

const ProtectedRoute = ({ component: Component, isAuthenticated, ...rest }) => (
  <Route
    {...rest}
    render={props =>
      isAuthenticated ? (
        <Component {...props} />
      ) : (
          <Redirect
            to={{
              pathname: "/login",
              state: { from: props.location }
            }}
          />
        )
    } />
);

class App extends Component {
  render() {
  const authenticated = !!this.props.userStore.token;
    return (<Layout isAuthenticated={authenticated}>
      <Route exact path='/' component={Home} />
      <Route exact path='/login' component={Login} />
      <Route exact path='/register' component={Register} />
      <ProtectedRoute exact path='/contestants' isAuthenticated={authenticated} component={ContestantList} />
      <ProtectedRoute path='/contestants/:id' isAuthenticated={authenticated} component={ContestantProfile} />
    </Layout>);
  }
}

export default connect(state => state)(App);