import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { actionCreators } from '../store/UserStore';
import {
  FormGroup,
  FormFeedback,
  Input,
  Label,
  Button,
  Row,
  Col,
} from 'reactstrap';

import './Register.css';

class Register extends Component {
  handleChange = (e, name) => {
    this.props.updateField(name, e.target.value);
  }

  render() {
    const { username, email, firstName, lastName, password, confirmPassword, submittedOnce } = this.props;
    if (this.props.token) {
      return (<Redirect to='/' />)
    }

    const validation = {
      username: {
        invalid: username === '' && submittedOnce,
        valid: username !== '' && submittedOnce,
        text: ""
      },
      email: {
        invalid: email === '' && submittedOnce,
        valid: email !== '' && submittedOnce,
        text: ""
      },
      firstName: {
        invalid: firstName === '' && submittedOnce,
        valid: firstName !== '' && submittedOnce,
        text: ""
      },
      lastName: {
        invalid: lastName === '' && submittedOnce,
        valid: lastName !== '' && submittedOnce,
        text: ""
      },
      password: {
        invalid: password === '' && submittedOnce,
        valid: password !== '' && submittedOnce,
        text: ""
      },
      confirmPassword: {
        invalid: submittedOnce && (confirmPassword === '' || confirmPassword !== password),
        valid: submittedOnce && confirmPassword !== '' && confirmPassword === password,
        text: confirmPassword !== password ? "Passwords do not match" : ""
      },
    }



    return (<div className={'sign-in-form-wrapper'}>
      <div className={'sign-in-form'}>
        <Row>
          <Col xs="12" md={{ size: 4, offset: 4 }}>
            <h2>Register</h2>
          </Col>
        </Row>
        <Row>
          <Col xs="12" md={{ size: 4, offset: 4 }}>
            <FormGroup>
              <Label for="username" className={'sr-only'}>Username</Label>
              <Input
                type="text"
                name="username"
                placeholder="Username"
                invalid={validation.username.invalid}
                valid={validation.username.valid}
                value={username}
                onChange={e => this.handleChange(e, "username")} />
              <FormFeedback>{validation.username.text}</FormFeedback>
            </FormGroup>
          </Col>
        </Row>
        <Row>
          <Col xs="12" md={{ size: 4, offset: 4 }}>
            <FormGroup>
              <Label for="email" className={'sr-only'}>Email</Label>
              <Input
                type="email"
                name="email"
                placeholder="Email"
                invalid={validation.email.invalid}
                valid={validation.email.valid}
                value={this.props.email}
                onChange={e => this.handleChange(e, "email")} />
              <FormFeedback>{validation.email.text}</FormFeedback>
            </FormGroup>
          </Col>
        </Row>
        <Row>
          <Col xs="12" md={{ size: 4, offset: 4 }}>
            <FormGroup>
              <Label for="firstName" className={'sr-only'}>First Name</Label>
              <Input
                type="text"
                name="firstName"
                placeholder="First Name"
                invalid={validation.firstName.invalid}
                valid={validation.firstName.valid}
                value={this.props.firstName}
                onChange={e => this.handleChange(e, "firstName")} />
              <FormFeedback>{validation.firstName.text}</FormFeedback>
            </FormGroup>
          </Col>
        </Row>
        <Row>
          <Col xs="12" md={{ size: 4, offset: 4 }}>
            <FormGroup>
              <Label for="firstName" className={'sr-only'}>Last Name</Label>
              <Input
                type="text"
                name="lastName"
                placeholder="Last Name"
                invalid={validation.lastName.invalid}
                valid={validation.lastName.valid}
                value={this.props.lastName}
                onChange={e => this.handleChange(e, "lastName")} />
              <FormFeedback>{validation.lastName.text}</FormFeedback>
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
                invalid={validation.password.invalid}
                valid={validation.password.valid}
                value={this.props.password}
                onChange={e => this.handleChange(e, "password")} />
              <FormFeedback>{validation.password.text}</FormFeedback>
            </FormGroup>
          </Col>
        </Row>
        <Row>
          <Col xs="12" md={{ size: 4, offset: 4 }}>
            <FormGroup>
              <Label for="password" className={'sr-only'}>Confirm Password</Label>
              <Input
                type="password"
                name="password"
                placeholder="Confirm Password"
                invalid={validation.confirmPassword.invalid}
                valid={validation.confirmPassword.valid}
                value={this.props.confirmPassword}
                onChange={e => this.handleChange(e, "confirmPassword")} />
              <FormFeedback>{validation.confirmPassword.text}</FormFeedback>
            </FormGroup>
          </Col>
        </Row>
        <Row className={'text-center'}>
          <Col xs="12" md={{ size: 4, offset: 4 }}>
            <Button className={'login-button'} color="primary" onClick={this.props.register}>Register</Button>
          </Col>
        </Row>
      </div>
    </div>)
  }
}

export default connect(
  state => state.userStore,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Register);