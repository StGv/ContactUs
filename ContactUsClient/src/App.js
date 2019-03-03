import React, { Component } from 'react';
import ContactUsForm from './components/contactUsForm'
import 'semantic-ui-css/semantic.min.css';

class App extends Component {
   state = {
    messageMaxLength:2500,
    messageLen:2500,
    sending:false,
    error:'',
    success:undefined,
    firstName:'',
    lastName:'',
    message:'',
  }

  onSendMessage = async () => {
    const url = `http://localhost:49341/api/contactus`;
    const init = {
      method: 'POST',
      mode: "cors", 
      cache: "no-cache",
      credentials: "same-origin", 
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        fullname: this.state.firstName + ' ' + this.state.lastName,
        email: this.state.email,
        message: this.state.message,
      })
    };

    this.setState({...this.state, sending:true})

    fetch(url, init)
    .then(response => {
      if(response.ok) {
        return response.json();
      }
      throw new Error('Network response was not OK.');
    })
    .then(json => this.setState({...this.state, success:true, error:'', sending:false}))
    .catch((error) => this.setState({...this.state, sending:false, success:false, error}));
  }

  onFirstNameChange = (e, {value}) => this.setState({...this.state, firstName:value});
  onLastNameChange = (e, {value}) => this.setState({...this.state, lastName:value});
  onEmailChange = (e, {value}) => this.setState({...this.state, email:value});
  onMessageChange = (e, {value}) => this.setState({...this.state, message:value, messageLen:this.state.messageMaxLength-value.length});

  render() {
    return <ContactUsForm {...this.state}
      onFirstNameChange1={this.onFirstNameChange}
      onLastNameChange={this.onLastNameChange}
      onEmailChange={this.onEmailChange}
      onMessageChange={this.onMessageChange}
      onSendMessage={this.onSendMessage} 
      />
  }
}

export default App;
