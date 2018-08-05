import React, { Component } from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
import NavMenu from './NavMenu';

export default class Layout extends Component {
    render() {
        return (<Grid fluid>
            <Row>
                <Col md={3}>
                    <NavMenu />
                </Col>
                <Col md={9}>
                    {this.props.children}
                </Col>
            </Row>
        </Grid>);
    }
}
