import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { actionCreators } from '../store/UserStore';
import {
    Col,
    Row,
    FormControl,
    Button,
    ButtonToolbar
} from 'react-bootstrap';

class Login extends Component {
    handleChange = (e, name) => {
        this.props.updateField(name, e.target.value);
    }

    render() {
        if (this.props.token){
            return (<Redirect to='/' />)
        }

        return (<div>
            <Row>
                <Col xs={12} md={4}>
                    <FormControl
                        type="text"
                        placeholder="Username"
                        onChange={(e) => this.handleChange(e, "username")}
                        value={this.props.username}
                    />
                </Col>
            </Row>
            <Row>
                <Col xs={12} md={4}>
                    <FormControl 
                        type="password" 
                        placeholder="Password"
                        onChange={(e) => this.handleChange(e, "password")}
                        value={this.props.password}
                    />
                </Col>
            </Row>
            <Row>
                <Col xs={12} md={4}>
                    <ButtonToolbar>
                        <Button bsStyle="link">Forgot password?</Button>
                    </ButtonToolbar>
                </Col>
            </Row>
            <Row>
                <Col xs={12} md={4}>
                    <ButtonToolbar>
                        <Button bsStyle="default">Create An Account</Button>
                        <Button bsStyle="primary" onClick={this.props.login}>Submit</Button>
                    </ButtonToolbar>
                </Col>
            </Row>
        </div>)
    }
}

export default connect(
    state => state.userStore,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Login);