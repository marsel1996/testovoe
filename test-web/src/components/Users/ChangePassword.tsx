import { Registry } from "../../api/TestApi";
import MyButton from "../Shared/MyButton";
import { Form, Input, Modal } from 'antd';
import { ModalComponent } from "../../models/ModalComponentProps";
import { useState } from "react";
import { requiredRule } from "../../models/InputRules";
import { Rule } from "rc-field-form/lib/interface";

type ChangePasswordUserProps = ModalComponent & {
    id: number,
}

const ChangePassword = ({id, onComplete}: ChangePasswordUserProps): JSX.Element => {
    const service: Registry.UserService = new Registry.UserService();

    const [form] = Form.useForm();
    const [password, setPassword] = useState<string>('');
    const [passwordAccess, setPasswordAccess] = useState<string>('');
    const save: () => Promise<void> = async () => {        
        const valid = await form.validateFields();
        if(valid.errorFields) {
            return
        }
        try{
            await service.editPassword(new Registry.EditPasswordCommand({id: id, password}));
            onComplete(true);
        } catch(e) {

        }
    }

    return (
        <Modal title="Изменить пароль" visible={true} 
            onCancel={() => onComplete(false)}
            footer={[
                <MyButton onClick={save}>
                    Сохранить
                </MyButton>,
                <MyButton onClick={() => onComplete(false)}>
                    Закрыть
                </MyButton>
            ]}>
            <Form
                name="basic"
                labelCol={{ span: 8 }}
                wrapperCol={{ span: 16 }}
                initialValues={{ remember: true }}
                autoComplete="off"
                form={form}
                >
                <Form.Item
                    label="Новый пароль:"
                    name="password"
                    rules={[requiredRule]}
                >
                    <Input value={password}
                        autoFocus
                        type="password"
                        onChange={(e) => setPassword(e.target.value)}/>
                </Form.Item>
                <Form.Item
                    label="Подтвердите пароль:"
                    name="passwordAccess"
                    rules={[requiredRule, { transform: () => password !== passwordAccess || 'Неверный пароль', message: 'Неверный пароль' }]}
                >
                    <Input value={passwordAccess}
                        autoFocus
                        type="password"
                        onChange={(e) => setPasswordAccess(e.target.value)}/>
                </Form.Item>
            </Form>    
        </Modal>    
    );
}

export default ChangePassword;