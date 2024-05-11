import { toast } from 'react-toastify';

export const createOnError = (text: string) => () => {
  toast(text, {
    type: 'error'
  });
};

export const createOnSuccess = (text: string) => () => {
  toast(text, {
    type: 'success'
  });
};

export const addSuccessToast = (text: string) => {
  toast(text, {
    type: 'success'
  });
};

export const addErrorToast = (text: string) => {
  toast(text, {
    type: 'error'
  });
};
