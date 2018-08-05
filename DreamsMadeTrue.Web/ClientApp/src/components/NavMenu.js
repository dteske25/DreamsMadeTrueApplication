import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
} from 'reactstrap';

import './NavMenu.css';

class NavMenu extends Component {
  constructor(props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.state = {
      isOpen: false
    };
  }

  toggle() {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }

  logout() {
    localStorage.clear();
  }

  render() {
    var authenticationDependentItems = this.props.isAuthenticated ? [
      <NavItem key={'contestant'}>
        <NavLink tag={Link} to="/contestants">Contestants</NavLink>
      </NavItem>,
      <NavItem key={'logout'}>
        <NavLink onClick={this.logout} href="/">Logout</NavLink>
      </NavItem>
    ] : [
        <NavItem key={'login'}>
          <NavLink tag={Link} to="/login">Login</NavLink>
        </NavItem>
      ];

    return (
      <div className={'navbar-wrapper'}>
        <Navbar color="dark" dark expand="md">
          <NavbarBrand tag={Link} to="/">Dreams Made True</NavbarBrand>
          <NavbarToggler onClick={this.toggle} />
          <Collapse isOpen={this.state.isOpen} navbar>
            <Nav>
              <NavItem>
                <NavLink tag={Link} to="/">Home</NavLink>
              </NavItem>
              {authenticationDependentItems}
            </Nav>
          </Collapse>
        </Navbar>
      </div>
    );
  }
}

export default connect(
  (state, ownProps) => {
    return {
      isAuthenticated: ownProps.isAuthenticated,
    }
  },
)(NavMenu)