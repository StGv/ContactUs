import React, { Component } from 'react';
import './App.css';
import { Button, Form , TextArea, Message} from 'semantic-ui-react';
import 'semantic-ui-css/semantic.min.css';

class App extends Component {
  messageMaxLength = 2500;
  state = {
    messageLen:this.messageMaxLength,
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

  render() {
    console.log(this.state)

    return (
      <div>
        <Form className={this.state.success===true ? "success" : "warning"} onSubmit={e => e.preventDefault()} >
          <Form.Input required
            label='First Name' 
            placeholder='First Name' 
            type="text"
            maxLength={50}
            onChange={(e, {value}) => this.setState({...this.state, firstName:value})}
            />
          <Form.Input required
            label='Last Name' 
            placeholder='Last Name' 
            type="text" 
            maxLength={50}
            onChange={(e, {value}) => this.setState({...this.state, lastName:value})}
            />
          <Form.Input required
            label='Email' 
            placeholder='joe@schmoe.com' 
            type="email" 
            maxLength={50}
            onChange={(e, {value}) => this.setState({...this.state, email:value})}
            />
          <Form.Field required
            control={TextArea} 
            label='Message'
            placeholder='your message here...' 
            maxLength={this.messageMaxLength}
            onChange={(e, {value}) => this.setState({...this.state, message:value, messageLen:this.messageMaxLength-value.length})}
          />
          <label>{this.state.messageLen}/{this.messageMaxLength}</label>
          <br/>
          <Button  className={this.state.sending===true ? "loading" : ""} onClick={() => this.onSendMessage()}>
            Submit
          </Button>
          {(this.state.success===true) && <Message success header='Thank you!' content="Your message has been received!" />}
          {(this.state.success===false) && <Message warning header='Ooops!' content={this.state.error.toString()} />}
        </Form>
      </div>
    );
  }
}

export default App;
