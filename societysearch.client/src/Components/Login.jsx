import * as React from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import TextField from '@mui/material/TextField';

export default function Login({ open, onClose, onLogin }) {
  const handleClose = () => {
    onClose?.();
  };

  const handleLogin = () => {
    onLogin?.();
    handleClose();
  };

  return (
    <React.Fragment>
    
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
        role="alertdialog"
      >
        <DialogTitle id="alert-dialog-title">
          {"Manager Login"}
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            <TextField label="Username" variant="standard" sx={{width:"500px",height:"50px"}}/><br/><br/>
            <TextField label="Password" type="password" variant="standard" sx={{width:"500px",height:"50px" }} />
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          
          <Button type='submit' onClick={handleLogin}>Login</Button>
        </DialogActions>
      </Dialog>
    </React.Fragment>
  );
}
