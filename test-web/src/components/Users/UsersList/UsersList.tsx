import { Input, Space, Table } from "antd";
import { Row, Col } from 'antd';
import { useEffect, useState } from "react";
import { Registry } from "../../../api/TestApi";
import GetUserListItemDto = Registry.GetUserListItemDto;
import MyButton from "../../Shared/MyButton";
import styles from './UsersList.module.sass';
import Column from "antd/lib/table/Column";
import { EditOutlined, EyeOutlined, CloseOutlined, SlidersOutlined } from '@ant-design/icons';
import React, { Suspense } from "react";
const RemoveUser = React.lazy(() => import('../RemoveUser'));
const UserInfo = React.lazy(() => import('../UserInfo'));
const UserEditor = React.lazy(() => import('../UserEditor'));
const ChangePassword = React.lazy(() => import('../ChangePassword'));

type ModalMode = 'editor' | 'edit-password' | 'view-info' |  'remove';

const UsersList = ({...props}): JSX.Element => {
    const service: Registry.UserService = new Registry.UserService();
    const currentUserId = 1;
    let [users, setUsers] = useState<GetUserListItemDto[]>([]);
    let [selectedUser, setSelectedUser] = useState<GetUserListItemDto | null>(null);
    let [mode, setMode] = useState<ModalMode | null>(null);
    let [filterName, setFilterName] = useState<string>('');

    const load: () => Promise<void> = async () => {
        const users = await service.getUserList(filterName);
        setUsers(users);
    }

    useEffect(() => {
        load();
    }, []);
    
    const setModalMode = (mode: ModalMode | null, user: GetUserListItemDto | null) => {
        setSelectedUser(user);
        setMode(mode);
    }

    const complete = async (update: boolean) => {
        setModalMode(null, null);
        if(update) {
            await load();
        }
    }

    return (
        <div className={styles.users}>
            <Row>
                <Col>
                    <h1>Список пользователей</h1>
                </Col>
            </Row>
            <Row gutter={10}>
                <Col xs={{span: 12, order: 1}} lg={{span: 6, order: 1}}>
                    <Input value={filterName} onChange={(e) => setFilterName(e.target.value)} onKeyUp={(e) => e.keyCode === 13 && load()}></Input>
                </Col>
                <Col xs={{span: 6, order: 1}} lg={{span: 2, order: 1}}>                                        
                    <MyButton onClick={load}>
                        Обновить
                    </MyButton>
                </Col>
                <Col xs={{span: 24, order: 3}} lg={{span: 22, order: 2}}>            
                    <Table dataSource={users} rowKey="id" pagination={{position: []}}>
                        <Column title="Полное имя" 
                            dataIndex="fullName" 
                            key="fullName" 
                            width="40%"
                            sorter={(record1: GetUserListItemDto, record2: GetUserListItemDto) => record1.fullName!.localeCompare(record2.fullName!)}
                            />
                        <Column title="Email" dataIndex="email" key="email" width="25%" 
                            sorter={(record1: GetUserListItemDto, record2: GetUserListItemDto) => record1.email!.localeCompare(record2.email!)}/>
                        <Column title="Телефон" dataIndex="phone" key="phone" width="25%" />
                        <Column width="10%"
                        title="Действия"
                        key="action"
                        render={(text, record: GetUserListItemDto) => (
                            <Space size="middle">
                                <MyButton type="text" icon={<EyeOutlined />} onClick={() => setModalMode('view-info', record)}></MyButton>
                                <MyButton type="text" icon={<EditOutlined />} style={{color: "#1890ff"}} onClick={() => setModalMode('editor', record)}></MyButton>
                                <MyButton type="text" icon={<CloseOutlined style={{color: "red"}}/>} onClick={() => setModalMode('remove', record)} disabled={record.id === currentUserId}></MyButton>
                                <MyButton type="text" icon={<SlidersOutlined style={{color: "#1890ff"}}/>} onClick={() => setModalMode('edit-password', record)}></MyButton>
                            </Space>
                        )}
                        />
                    </Table>
                </Col>
                <Col xs={{span: 6, order: 2}} lg={{span: 2, order: 3}}>
                    <MyButton onClick={() => setModalMode('editor', null)}>
                        Добавить
                    </MyButton>
                </Col>
            </Row>
            {
                selectedUser && mode === "remove"
                ?   <Suspense fallback={<div>Loading...</div>}>
                        <RemoveUser id={selectedUser.id} fullName={selectedUser.fullName || ''} onComplete={complete}></RemoveUser>
                    </Suspense>
                : ''
            }
            {
                selectedUser && mode === "view-info"
                ?   <Suspense fallback={<div>Loading...</div>}>
                        <UserInfo key="info" id={selectedUser.id} onComplete={complete}></UserInfo>
                    </Suspense>
                : ''
            }
            {
                selectedUser && mode === "edit-password"
                ?   <Suspense fallback={<div>Loading...</div>}>
                        <ChangePassword key="editor" id={selectedUser.id} onComplete={complete}></ChangePassword>
                    </Suspense>
                : ''
            }
            {
                mode === "editor"
                ?   <Suspense fallback={<div>Loading...</div>}>
                        <UserEditor key="editor" id={selectedUser?.id || null} onComplete={complete}></UserEditor>
                    </Suspense>
                : ''
            }
        </div>
    );
}

export default UsersList;