﻿// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 可删除仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public partial interface IDeletableRepository<TEntity> : IDeletableRepository<TEntity, MasterDbContextLocator>
    where TEntity : class, IPrivateEntity, new()
{
}

/// <summary>
/// 可删除仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
public partial interface IDeletableRepository<TEntity, TDbContextLocator> : IPrivateDeletableRepository<TEntity>
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator : class, IDbContextLocator
{
}

/// <summary>
/// 可删除仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IPrivateDeletableRepository<TEntity> : IPrivateRootRepository
    where TEntity : class, IPrivateEntity, new()
{
    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>代理中的实体</returns>
    EntityEntry<TEntity> Delete(TEntity entity);

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    void Delete(params TEntity[] entities);

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    void Delete(IEnumerable<TEntity> entities);

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>代理中的实体</returns>
    Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity);

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <returns>Task</returns>
    Task DeleteAsync(params TEntity[] entities);

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <returns>Task</returns>
    Task DeleteAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>代理中的实体</returns>
    EntityEntry<TEntity> DeleteNow(TEntity entity);

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <returns></returns>
    EntityEntry<TEntity> DeleteNow(TEntity entity, bool acceptAllChangesOnSuccess);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    void DeleteNow(params TEntity[] entities);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    void DeleteNow(TEntity[] entities, bool acceptAllChangesOnSuccess);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    void DeleteNow(IEnumerable<TEntity> entities);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    void DeleteNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess);

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>代理中的实体</returns>
    Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>代理中的实体</returns>
    Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <returns>Task</returns>
    Task DeleteNowAsync(params TEntity[] entities);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    Task DeleteNowAsync(TEntity[] entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    Task DeleteNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    Task DeleteNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    Task DeleteNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据主键删除一条记录
    /// </summary>
    /// <param name="key">主键</param>
    void Delete(object key);

    /// <summary>
    /// 根据主键删除一条记录
    /// </summary>
    /// <param name="key">主键</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>Task</returns>
    Task DeleteAsync(object key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据主键删除一条记录并立即提交
    /// </summary>
    /// <param name="key">主键</param>
    void DeleteNow(object key);

    /// <summary>
    /// 根据主键删除一条记录并立即提交
    /// </summary>
    /// <param name="key">主键</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    void DeleteNow(object key, bool acceptAllChangesOnSuccess);

    /// <summary>
    /// 根据主键删除一条记录并立即提交
    /// </summary>
    /// <param name="key">主键</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns></returns>
    Task DeleteNowAsync(object key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据主键删除一条记录并立即提交
    /// </summary>
    /// <param name="key">主键</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns></returns>
    Task DeleteNowAsync(object key, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
}