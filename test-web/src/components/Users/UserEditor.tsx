import { Registry } from "../../api/TestApi";
import MyButton from "../Shared/MyButton";
import { Alert, Modal, Select } from 'antd';
import { useEffect, useMemo, useState } from "react";
import { Form, Input } from 'antd';
import { ModalComponent } from "../../models/ModalComponentProps";
import { emailRule, phoneRule, requiredRule } from "../../models/InputRules";
import { UserData } from "../../models/UserData";
const { Option } = Select;

type UserEditorProps = ModalComponent & {
    id: number | null,
}

const UserEditor = ({id, onComplete}: UserEditorProps): JSX.Element => {
    const service: Registry.UserService = new Registry.UserService();
    
    const [userData, setUserData] = useState<UserData>({});
    const [roles, setRoles] = useState<Registry.GetUserRolesListItemDto[]>([]);
    const [error, setError] = useState<string | null>(null);
    const [form] = Form.useForm();

    const loadData = async () => {
        if (id) {
            const user = await service.getUserById(id);               
            setUserData({
                id: user.id,
                email: user.email,
                firstName: user.firstName,
                phone: user.phone,
                roleId: user.roleId,
                surName: user.surName,
            });
            console.log('awdawdaw')
        }
        const roles = await service.getUserRoleList(null);
        setRoles(roles);
    }

    const roleOptions = useMemo(() => {
        return roles.map(role => <Option key={role.id} value={role.id}>{role.name}</Option>);
    }, [roles]);
    
    const save = async () => {
        const valid = await form.validateFields();
        if(valid.errorFields) {
            return
        }
        try {
            if (id) {                    
                await service.editUser(new Registry.EditUserCommand({
                    email: userData.email || null,
                    firstName: userData.firstName || null,
                    phone: userData.phone || null,
                    role: userData.roleId!,
                    surName: userData.surName || null,
                    id: id,
                }));
            } else {
                await service.saveUser(new Registry.SaveUserCommand({
                    email: userData.email || null,
                    firstName: userData.firstName || null,
                    password: userData.password!,
                    phone: userData.phone || null,
                    role: userData.roleId!,
                    surName: userData.surName || null,
                }));
            };
            onComplete(true)
        } catch(e: any) {
            setError(e.message || 'Ошибка при сохранении.');
            setTimeout(() => setError(null), 2000);
        }
    }

    useEffect(() => {
        loadData();
    }, []);

    return (
        <Modal title={id ? 'Изменение данных' : 'Добавление нового пользователя'} visible={true} 
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
                labelCol={{ span: 4 }}
                wrapperCol={{ span: 20 }}
                form={form}
                >
                {
                    error !== null ? <Alert message={error} type="warning" style={{marginBottom: 20}}/> : ''
                }
                <Form.Item
                    label="Фамилия:"
                    name="surName"
                    rules={[requiredRule]}
                >
                    <Input value={userData.surName || ''}
                        onChange={(e) => setUserData({...userData, surName: e.target.value})}/>
                </Form.Item>
                <Form.Item
                    label="Имя:"
                    name="firstName"
                    rules={[requiredRule]}
                >
                    <Input value={userData.firstName || ''}
                        onChange={(e) => setUserData({...userData, firstName: e.target.value})}/>
                </Form.Item>                
                {
                    id 
                    ? '' 
                    :<Form.Item
                        label="Пароль:"
                        name="password"
                        rules={[requiredRule]}
                    >
                        <Input value={userData.password || ''}
                            type="password"
                            onChange={(e) => setUserData({...userData, password: e.target.value})}/>
                    </Form.Item>
                }
                <Form.Item
                    label="Email:"
                    name="email"
                    rules={[requiredRule, emailRule]}
                >
                    <Input value={userData.email || ''}
                        onChange={(e) =>  setUserData({...userData, email: e.target.value})}/>
                </Form.Item>
                <Form.Item
                    label="Телефон:"
                    name="phone"
                    rules={[phoneRule]}
                >
                    <Input value={userData.phone || ''}
                        onChange={(e) =>  setUserData({...userData, phone: e.target.value})}/>
                </Form.Item>
                <Form.Item
                    label="Роль:"
                    name="role"
                    rules={[requiredRule]}
                >
                    <Select
                        placeholder="Выберите роль"
                        value={userData.roleId || undefined}
                        onChange={(val) =>  setUserData({...userData, roleId: Number(val)})}
                        allowClear
                        >
                        {roleOptions}
                    </Select>
                </Form.Item>
            </Form>     
        </Modal>   
    );
}

export default UserEditor;