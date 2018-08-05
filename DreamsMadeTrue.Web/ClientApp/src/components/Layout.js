import React, { Component } from 'react';
import NavMenu from './NavMenu';
import { Container, Row, Col } from 'reactstrap';

export default class Layout extends Component {
  render() {
    return (
      <Container fluid>
        <Row>
          <NavMenu isAuthenticated={this.props.isAuthenticated} />
        </Row>
        <Row>
          <Col>
            {this.props.children}
          </Col>
        </Row>
      </Container>
    );
  }
}
