import React, { Component } from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Auth from './components/Auth';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import Login from './components/Login';
import ContestantList from './components/ContestantList';
import ContestantProfile from './components/ContestantProfile';

export default class App extends Component {
    render() {
        return (<Layout>
            <Auth>
                <Route exact path='/' component={Home} />
                <Route path='/counter' component={Counter} />
                <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
                <Route path='/login' component={Login} />
                <Route exact path='/contestants' component={ContestantList} />
                <Route path='/contestants/:id' component={ContestantProfile} />
            </Auth>
        </Layout>);
    }
}

