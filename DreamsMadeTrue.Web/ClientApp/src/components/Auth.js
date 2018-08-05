import React, {Component} from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';

class Auth extends Component {
  render () {
    if (!this.props.userStore.token && this.props.routing.location.pathname !== '/login'){
      return (<Redirect to='/login'/>);
    }
    return (<div>{this.props.children}</div>)
  }
}

export default connect (
  state => state
)(Auth);