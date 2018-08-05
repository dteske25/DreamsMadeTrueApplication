import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { Redirect, Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { actionCreators } from '../store/UserStore';
import {
  FormGroup,
  Input,
  Label,
  Button,
  Row,
  Col,
} from 'reactstrap';

import './Login.css';

class Login extends Component {
  handleChange = (e, name) => {
    this.props.updateField(name, e.target.value);
  }

  render() {
    if (this.props.token) {
      return (<Redirect to='/' />)
    }

    return (<div className={'text-center sign-in-form-wrapper'}>
      <div className={'sign-in-form'}>
        <Row>
          <Col>
            <h2>Sign in</h2>
          </Col>
        </Row>
        <Row>
          <Col xs="12" md={{ size: 4, offset: 4}}>
            <FormGroup>
              <Label for="username" className={'sr-only'}>Username</Label>
              <Input
                type="text"
                name="username"
                placeholder="Username"
                required
                value={this.props.username}
                onChange={e => this.handleChange(e, "username")} />
            </FormGroup>
          </Col>
        </Row>
        <Row>
          <Col xs="12" md={{ size: 4, offset: 4 }}>
            <FormGroup>
              <Label for="password" className={'sr-only'}>Password</Label>
              <Input
                type="password"
                name="password"
                placeholder="Password"
                required
                value={this.props.password}
                onChange={e => this.handleChange(e, "password")} />
            </FormGroup>
          </Col>
        </Row>
        <Row>
          <Col xs="12" md={{ size: 4, offset: 4 }}>
            <Button color="link">Forgot password?</Button>
          </Col>
        </Row>
        <Row>
          <Col xs="12" md={{ size: 4, offset: 4 }}>
            <Link to={'/register'}>
              <Button className={'register-button'} color="secondary">Register</Button>
            </Link>
            <Button className={'login-button'} color="primary" onClick={this.props.login}>Log In</Button>
          </Col>
        </Row>
      </div>
    </div>)
  }
}

export default connect(
  state => state.userStore,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Login);