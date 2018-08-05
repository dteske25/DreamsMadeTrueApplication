import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import {
  Table,
} from 'reactstrap';

class ContestantList extends Component {
  render() {

    const items = this.props.list.map(c => {
      return (<tr key={c.id}><td><Link to={`/contestants/${c.id}`}>{c.firstName} {c.lastName}</Link></td><td>{c.birthday.format('MMM Do YYYY')}</td></tr>);
    });

    return (<Table className={'table-hover'}>
      <thead>
        <tr>
          <th>Name</th>
          <th>Birthday</th>
        </tr>
      </thead>
      <tbody>
        {items}
      </tbody>
    </Table>);
  }
}

export default connect(
  state => state.contestantStore
)(ContestantList);