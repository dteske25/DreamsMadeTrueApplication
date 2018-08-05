import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Row, Col } from 'react-bootstrap';
import * as moment from 'moment';

class ContestantProfile extends Component {
  render() {
    var id = this.props.match.params.id;
    var contestant = null;
    for (var c of this.props.list) {
      if (c.id === id) {
        contestant = c;
        break;
      }
    }
    var years = moment().diff(contestant.birthday, 'years', true);


    return (<div>
      <Row>
        <Col md={8}>
          <p>Name: {contestant.firstName} {contestant.lastName}</p>
          <br/>
          <p>Age: {Math.floor(years)} years old</p>
        </Col>
      </Row>
    </div>);
  }
}

export default connect(
  state => state.contestantStore
)(ContestantProfile);