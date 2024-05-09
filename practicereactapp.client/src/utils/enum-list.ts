import { forIn } from 'lodash';

export interface IEnumItem {
    key: string;
    value: string;
}

export function getEnumList<T>(type: T): IEnumItem[] {
    const list: IEnumItem[] = [];
    forIn(type, (value, key) => {
        if (isNaN(Number(key))) {
            list.push({ key: key, value: String(value) });
        }
    });

    return list;
}

export function getEnumValueList<T>(type: T): string[] {
    return getEnumList(type).map(x => x.value);
}