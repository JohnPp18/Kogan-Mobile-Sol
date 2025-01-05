export default interface IGetSingleEntityService<T> {
    getSingleAsync(id: int): Promise<T>;
}