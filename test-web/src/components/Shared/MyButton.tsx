import { Button, ButtonProps } from 'antd';

const MyButton = ({children, ...props}: ButtonProps): JSX.Element => {
    const defaultProps: ButtonProps = {
        type: 'primary',
        size: 'middle'
    };

    const buttonProps: ButtonProps = Object.assign({}, defaultProps, props);

    return (
        <Button {...buttonProps}>
            {children}
        </Button>
    );
}

export default MyButton;