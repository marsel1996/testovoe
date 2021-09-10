import { Registry } from "../../api/TestApi";
import MyButton from "../Shared/MyButton";
import { Modal } from 'antd';
import { ModalComponent } from "../../models/ModalComponentProps";

type RemoveUserProps = ModalComponent & {
    id: number,
    fullName: string,
}

const RemoveUser = ({id, fullName, onComplete}: RemoveUserProps): JSX.Element => {
    const service: Registry.UserService = new Registry.UserService();

    const remove: () => Promise<void> = async () => {
        try{
            await service.removeUser(id);
            onComplete(true);
        } catch(e) {

        }
    }

    return (
        <Modal title="Предупреждение!" visible={true} 
            onCancel={() => onComplete(false)}
            footer={[
                <MyButton onClick={remove}>
                    Удалить
                </MyButton>,
                <MyButton onClick={() => onComplete(false)}>
                    Отмена
                </MyButton>
            ]}>
            <div>
                <span>
                    Вы дейсчтвительно желаете удалить пользователя ({fullName}) ?
                </span>
            </div>
        </Modal>        
    );
}

export default RemoveUser;