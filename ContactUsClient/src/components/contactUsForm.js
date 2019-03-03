import { Button, Form , TextArea, Message} from 'semantic-ui-react';
import React from 'react';

const ContactUsForm = (props) => {
    return <Form className={props.success===true ? "success" : "warning"} onSubmit={e => e.preventDefault()} >
    <Form.Input required
      label='First Name' 
      placeholder='First Name' 
      type="text"
      maxLength={50}
      onChange={props.onFirstNameChange}
      />
    <Form.Input required
      label='Last Name' 
      placeholder='Last Name' 
      type="text" 
      maxLength={50}
      onChange={props.onLastNameChange}
      />
    <Form.Input required
      label='Email' 
      placeholder='joe@schmoe.com' 
      type="email" 
      maxLength={50}
      onChange={props.onEmailChange}
      />
    <Form.Field required
      control={TextArea} 
      label='Message'
      placeholder='your message here...' 
      maxLength={props.messageMaxLength}
      onChange={props.onMessageChange}
    />
    <label>{props.messageLen}/{props.messageMaxLength}</label>
    <br/>
    <Button  className={props.sending===true ? "loading" : ""} onClick={() => props.onSendMessage()}>
      Submit
    </Button>
    {(props.success===true) && <Message success header='Thank you!' content="Your message has been received!" />}
    {(props.success===false) && <Message warning header='Ooops!' content={props.error.toString()} />}
  </Form>
  }
  
  export default ContactUsForm;
  